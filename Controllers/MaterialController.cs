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
    [System.Web.Http.RoutePrefix("FastMan/Material")]
    public class MaterialController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Material> acu = new List<Material>();
            try
            {

                acu = db.Material.Where(x=> x.Activo_Inactivo==true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Material");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelMaterial Id)
        {
            Material acu = new Material();
            try
            {
                if (Id != null)
                { acu = db.Material.Where(x => x.IdMaterial.Equals(Id.IdMaterial, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault(); }
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Material");
            }

        }
        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Edit(MaterialModel material)
        {
            Material acu = new Material();
            try
            {
                if (material != null)
                {
                    acu = db.Material.Where(x => x.IdMaterial.Equals(material.IdMaterial, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (acu != null)
                    {
                        acu.IdMaterial = material.IdMaterial;
                        acu.IdArea = material.IdArea;
                        acu.IdUnidadMedida = material.IdUnidadMedida;
                        acu.NumeroParteMaterial = material.NumeroParteMaterial;
                        acu.Nombre = material.Nombre;
                        acu.Marca = material.Marca;
                        acu.Posicion = material.Posicion;
                        acu.CostoUnidad = material.CostoUnidad;
                        acu.NoSerie = material.NoSerie;
                        acu.Cantidad = material.Cantidad;

                        db.SaveChanges();
                        return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                    }
                    else
                    {
                        return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                    }
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Material");
            }

        }
        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(MaterialModel material)
        {
            Material acu = new Material();
            try
            {
                if (material != null)
                {
                    acu.IdMaterial = material.IdMaterial;
                    acu.IdArea = material.IdArea;
                    acu.IdUnidadMedida = material.IdUnidadMedida;
                    acu.NumeroParteMaterial = material.NumeroParteMaterial;
                    acu.Nombre = material.Nombre;
                    acu.Marca = material.Marca;
                    acu.Posicion = material.Posicion;
                    acu.CostoUnidad = material.CostoUnidad;
                    acu.NoSerie = material.NoSerie;
                    acu.Cantidad = material.Cantidad;
                    acu.Activo_Inactivo = true;
                    db.Material.Add(acu);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotAcceptable) { Content = null };
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Material");
            }

        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(IdModelMaterial Id)
        {
            Material acu = new Material();
            try
            {
                if (Id != null)
                {
                    acu = db.Material.Where(x => x.IdMaterial.Equals(Id.IdMaterial, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
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
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Material");
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
        public class IdModelMaterial
        {
            [DataMember]
            public string IdMaterial { get; set; }
        }
    }

}
