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
    [System.Web.Http.RoutePrefix("FastMan/Areas")]
    public class AreasController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Area> acu = new List<Area>();
            try
            {
                acu = db.Area.Where(x=> x.Activo_Inactivo == true).ToList();
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
        public HttpResponseMessage Detail(IdModelAreas Id)
        {
            Area acu = new Area();
            try
            {
                acu = db.Area.Where(x => x.IdArea == Id.IdArea).FirstOrDefault();
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
        public HttpResponseMessage Edit(AreaModel area)
        {
            Area acu = new Area();
            try
            {

                acu = db.Area.Where(x => x.IdArea == area.IdArea).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdUbicacion = area.IdUbicacion;
                    acu.NombreArea = area.NombreArea;
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
        public HttpResponseMessage Create(AreaModel area)
        {
            Area acu = new Area();
            try
            {
                if (area != null)
                {
                    acu.IdUbicacion = area.IdUbicacion;
                    acu.NombreArea = area.NombreArea;
                    acu.Descripcion = area.Descripcion;
                    acu.Encargado = area.Encargado;
                    acu.Activo_Inactivo = true;
                    db.Area.Add(acu);
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
        public HttpResponseMessage Delete(IdModelAreas Id)
        {
            Area acu = new Area();
            try
            {

                acu = db.Area.Where(x => x.IdArea == Id.IdArea).FirstOrDefault();
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
        public class IdModelAreas
        {
            [DataMember]
            public int IdArea { get; set; }

        }

    }    

}
