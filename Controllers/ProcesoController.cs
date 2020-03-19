using System; using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using core.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace apifastman.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("FastMan/Proceso")]
    public class ProcesoController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Proceso> acu = new List<Proceso>();
            try
            {
                acu = db.Proceso.Where(x=> x.Activo_Inactivo == true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu),System.Text.Encoding.UTF8,"application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "area");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelProceso Id)
        {
            Proceso acu = new Proceso();
            try
            {
                acu = db.Proceso.Where(x => x.IdProceso == Id.IdProceso).FirstOrDefault();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Area");
            }

        }
        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Edit(ProcesoModel area)
        {
            Proceso acu = new Proceso();
            try
            {

                acu = db.Proceso.Where(x => x.IdProceso == area.IdProceso).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.NombreProceso = area.NombreProceso;
                    acu.Descripcion = area.Descripcion;
                    acu.Encargado = area.Encargado;
                    acu.Activo_Inactivo = area.Activo_Inactivo;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Area");
            }

        }
        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(ProcesoModel area)
        {
            Proceso acu = new Proceso();
            try
            {
                if (area != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.NombreProceso = area.NombreProceso;
                    acu.Descripcion = area.Descripcion;
                    acu.Encargado = area.Encargado;
                    acu.Activo_Inactivo = true;
                    db.Proceso.Add(acu);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Area");
            }

        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(IdModelProceso Id)
        {
            Proceso acu = new Proceso();
            try
            {

                acu = db.Proceso.Where(x => x.IdProceso == Id.IdProceso).FirstOrDefault();
                if (acu != null)
                {
                    acu.Activo_Inactivo = false;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Area");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelProceso
        {
            [DataMember]
            public int IdProceso { get; set; }

        }

    }    

}
