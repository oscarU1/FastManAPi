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
    [System.Web.Http.RoutePrefix("FastMan/Empresa")]
    public class EmpresaController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Empresa> acu = new List<Empresa>();
            try
            {
                acu = db.Empresa.Where(x => x.Activo_Inactivo == true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };


            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Empresa");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelEmpresa Id)
        {
            Empresa acu = new Empresa();
            try
            {

                if (Id.IdEmpresa != 0)
                    acu = db.Empresa.Where(x => x.IdEmpresa == Id.IdEmpresa).FirstOrDefault();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Empresa");
            }

        }
        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Edit(EmpresaModel empresa)
        {
            Empresa acu = new Empresa();
            try
            {
                acu = db.Empresa.Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                if (acu != null)
                {
                    acu.IdEmpresa = empresa.IdEmpresa;
                    acu.NombreEmpresa = empresa.NombreEmpresa;
                    acu.Calle = empresa.Calle;
                    acu.NoExterior = empresa.NoExterior;
                    acu.NoInterior = empresa.NoInterior;
                    acu.CodPostal = empresa.CodPostal;
                    acu.Colonia = empresa.Colonia;
                    acu.Localidad = empresa.Localidad;
                    acu.Municipio = empresa.Municipio;
                    acu.Estado = empresa.Estado;
                    acu.Pais = empresa.Pais;
                    acu.Telefono = empresa.Telefono;
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Empresa");
            }

        }
        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(EmpresaModel empresa)
        {
            Empresa acu = new Empresa();
            try
            {
                if (empresa != null)
                {
                    acu.IdEmpresa = empresa.IdEmpresa;
                    acu.NombreEmpresa = empresa.NombreEmpresa;
                    acu.Calle = empresa.Calle;
                    acu.NoExterior = empresa.NoExterior;
                    acu.NoInterior = empresa.NoInterior;
                    acu.CodPostal = empresa.CodPostal;
                    acu.Colonia = empresa.Colonia;
                    acu.Localidad = empresa.Localidad;
                    acu.Municipio = empresa.Municipio;
                    acu.Estado = empresa.Estado;
                    acu.Pais = empresa.Pais;
                    acu.Telefono = empresa.Telefono;
                    acu.Activo_Inactivo = true;
                    db.Empresa.Add(acu);
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Empresa");
            }

        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(IdModelEmpresa Id)
        {
            Empresa acu = new Empresa();
            try
            {

                acu = db.Empresa.Where(x => x.IdEmpresa == Id.IdEmpresa).FirstOrDefault();
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Empresa");
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
        public class IdModelEmpresa
        {
            [DataMember]
            public int IdEmpresa { get; set; }
        }
    }

}
