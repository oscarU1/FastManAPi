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
    [System.Web.Http.RoutePrefix("FastMan/SubProceso")]
    public class SubProcesoController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<SubProceso> acu = new List<SubProceso>();
            try
            {
                acu = db.SubProceso.Where(x=> x.Activo_Inactivo == true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu),System.Text.Encoding.UTF8,"application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "SubProceso");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelSubProceso Id)
        {
            SubProceso acu = new SubProceso();
            try
            {
                acu = db.SubProceso.Where(x => x.IdSubProceso == Id.IdSubProceso).FirstOrDefault();
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
        public HttpResponseMessage Edit(SubProcesoModel area)
        {
            SubProceso acu = new SubProceso();
            try
            {

                acu = db.SubProceso.Where(x => x.IdSubProceso == area.IdSubProceso).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdProceso = area.IdProceso;
                    acu.NombreSubProceso = area.NombreSubProceso;
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
        public HttpResponseMessage Create(SubProcesoModel area)
        {
            SubProceso acu = new SubProceso();
            try
            {
                if (area != null)
                {
                    acu.IdProceso = area.IdProceso;
                    acu.NombreSubProceso = area.NombreSubProceso;
                    acu.Descripcion = area.Descripcion;
                    acu.Encargado = area.Encargado;
                    acu.Activo_Inactivo = true;
                    db.SubProceso.Add(acu);
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
        public HttpResponseMessage Delete(IdModelSubProceso Id)
        {
            SubProceso acu = new SubProceso();
            try
            {

                acu = db.SubProceso.Where(x => x.IdSubProceso == Id.IdSubProceso).FirstOrDefault();
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
        public class IdModelSubProceso
        {
            [DataMember]
            public int IdSubProceso { get; set; }

        }

    }    

}
