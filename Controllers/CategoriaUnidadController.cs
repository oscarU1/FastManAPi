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
    [System.Web.Http.RoutePrefix("FastMan/CategoriaUnidad")]
    public class CategoriaUnidadController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<CategoriaUnidad> acu = new List<CategoriaUnidad>();
            try
            {
                acu = db.CategoriaUnidad.Where(x=> x.Activo_Inactivo == true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "CategoriaUnidad");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelCategoriaUnidad Id)
        {
            CategoriaUnidad acu = new CategoriaUnidad();
            try
            {
                acu = db.CategoriaUnidad.Where(x => x.IdCategoriaUnidad == Id.IdCategoriaUnidad).FirstOrDefault();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "CategoriaUnidad");
            }

        }
        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Edit(CategoriaUnidadModel area)
        {
            CategoriaUnidad acu = new CategoriaUnidad();
            try
            {

                acu = db.CategoriaUnidad.Where(x => x.IdCategoriaUnidad == area.IdCategoriaUnidad).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.CategoriaUnidad1 = area.CategoriaUnidad1;
                    acu.Descripcion = area.Descripcion;
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "CategoriaUnidad");
            }

        }
        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(CategoriaUnidadModel area)
        {
            CategoriaUnidad acu = new CategoriaUnidad();
            try
            {
                if (area != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.CategoriaUnidad1 = area.CategoriaUnidad1;
                    acu.Descripcion = area.Descripcion;
                    acu.Activo_Inactivo = true;
                    db.CategoriaUnidad.Add(acu);
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "CategoriaUnidad");
            }

        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(IdModelCategoriaUnidad Id)
        {
            CategoriaUnidad acu = new CategoriaUnidad();
            try
            {

                acu = db.CategoriaUnidad.Where(x => x.IdCategoriaUnidad == Id.IdCategoriaUnidad).FirstOrDefault();
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "CategoriaUnidad");
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
        public class IdModelCategoriaUnidad
        {
            [DataMember]
            public int IdCategoriaUnidad { get; set; }

        }
        [DataContract]
        public class CategoriaUnidadModel
        {
            [DataMember]
            public int IdCategoriaUnidad { get; set; }
            [DataMember]
            public int IdArea { get; set; }
            [DataMember]
            public string CategoriaUnidad1 { get; set; }
            [DataMember]
            public string Descripcion { get; set; }
            [DataMember]
            public Nullable<bool> Activo_Inactivo { get; set; }
        }

    }    

}
