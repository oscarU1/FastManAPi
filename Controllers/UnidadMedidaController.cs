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
    [System.Web.Http.RoutePrefix("FastMan/UnidadMedida")]
    public class UnidadMedidaController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<UnidadMedida> acu = new List<UnidadMedida>();
            try
            {
                acu = db.UnidadMedida.ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
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
        public HttpResponseMessage Detail(IdModelUnidadMedida Id)
        {
            UnidadMedida acu = new UnidadMedida();
            try
            {
                acu = db.UnidadMedida.Where(x => x.IdUbicacion == Id.IdUnidadMedida).FirstOrDefault();
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
        public HttpResponseMessage Edit(UnidadMedidaModel unid)
        {
            UnidadMedida acu = new UnidadMedida();
            try
            {

                acu = db.UnidadMedida.Where(x => x.IdUnidadMedida == unid.IdUnidadMedida).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdUnidadMedida = unid.IdUnidadMedida;
                    acu.IdUbicacion = unid.IdUbicacion;
                    acu.Nomenclatura = unid.Nomenclatura;
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
        public HttpResponseMessage Create(UnidadMedidaModel unid)
        {
            UnidadMedida acu = new UnidadMedida();
            try
            {
                if (unid != null)
                {
                    acu.IdUnidadMedida = unid.IdUnidadMedida;
                    acu.IdUbicacion = unid.IdUbicacion;
                    acu.Nomenclatura = unid.Nomenclatura;
                    db.UnidadMedida.Add(acu);
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
        public HttpResponseMessage Delete(IdModelUnidadMedida Id)
        {
            UnidadMedida acu = new UnidadMedida();
            try
            {

                acu = db.UnidadMedida.Where(x => x.IdUnidadMedida == Id.IdUnidadMedida).FirstOrDefault();
                if (acu != null)
                {
                    db.UnidadMedida.Remove(acu);
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
        public class IdModelUnidadMedida
        {
            [DataMember]
            public int IdUnidadMedida { get; set; }
        }
    }

}
