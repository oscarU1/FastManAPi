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
    [System.Web.Http.RoutePrefix("FastMan/GuiaServicio")]
    public class GuiaServicioController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<GuiaServicio> acu = new List<GuiaServicio>();
            List<GuiaServicioModel> ListservicioModels = new List<GuiaServicioModel>();
            GuiaServicioModel servicioModel;
            try
            {
                acu = db.GuiaServicio.Where(x=> x.Activo_Inactivo==true).ToList();
                if(acu != null)
                    foreach(GuiaServicio guia in acu)
                    {
                        servicioModel = new GuiaServicioModel();
                        var emp = db.Ubicacion.Where(x => x.IdUbicacion == guia.IdArea).Take(1).FirstOrDefault();
                        var marc = db.Marca.Where(x => x.IdMarca == guia.IdMarca).Take(1).FirstOrDefault();
                        var model = db.Modelo.Where(x => x.IdModelo == guia.IdModelo).Take(1).FirstOrDefault();
                        servicioModel.IdGuiaServicio = guia.IdGuiaServicio;
                        servicioModel.IdArea = guia.IdArea;
                        servicioModel.area = emp.Localidad;
                        servicioModel.IdMarca = guia.IdMarca;
                        servicioModel.Marca = marc.Descripcion;
                        servicioModel.IdModelo = guia.IdModelo;
                        servicioModel.Modelo = model.Descripcion;
                        servicioModel.Km_Hr = guia.Km_Hr;
                        servicioModel.Descripcion = guia.Descripcion;
                        servicioModel.Activo_Inactivo = guia.Activo_Inactivo;
                        ListservicioModels.Add(servicioModel);
                    }

                if (ListservicioModels != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(ListservicioModels), System.Text.Encoding.UTF8, "application/json") };
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
        public HttpResponseMessage Detail(IdModelGuiaServicioModel Id)
        {
            GuiaServicio guia = new GuiaServicio();
            try
            {

                if (Id != null)
                {
                    guia = db.GuiaServicio.Where(x => x.IdGuiaServicio == Id.IdGuiaServicio).FirstOrDefault();
                }
                if (guia != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(guia), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("CreateHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateHeader(GuiaServicioModel guiam)
        {

            GuiaServicio guiaServicio = new GuiaServicio();
            try
            {
                int idguia = 0;
                if (guiam != null)
                {
                    guiaServicio.IdArea = guiam.IdArea;
                    guiaServicio.IdMarca = guiam.IdMarca;
                    guiaServicio.IdModelo = guiam.IdModelo;
                    guiaServicio.Km_Hr = guiam.Km_Hr;
                    guiaServicio.Descripcion = guiam.Descripcion;
                    guiaServicio.Activo_Inactivo = true;
                    db.GuiaServicio.Add(guiaServicio);
                    db.SaveChanges();
                    var guia = db.GuiaServicio.Where(x => x.IdArea == guiaServicio.IdArea && x.IdMarca == guiaServicio.IdMarca && x.IdModelo == guiaServicio.IdModelo && x.Descripcion.Equals(guiaServicio.Descripcion, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    idguia = guia.IdGuiaServicio;
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(idguia), System.Text.Encoding.UTF8, "application/json") };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("CreateDetail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetail(DetalleGuiaServicioModel guiam)
        {

            DetalleGuiaServicio detalleGuia = new DetalleGuiaServicio();
            try
            {
                int idguia = 0;
                if (guiam != null)
                {
                    detalleGuia.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuia.Actividad = guiam.Actividad;
                    detalleGuia.Tiempo = guiam.Tiempo;
                    detalleGuia.Observaciones = guiam.Observaciones;
                    db.DetalleGuiaServicio.Add(detalleGuia);
                    db.SaveChanges();
                    var guia = db.DetalleGuiaServicio.Where(x => x.IdGuiaServicio == detalleGuia.IdGuiaServicio && x.Actividad.Equals(detalleGuia.Actividad, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    idguia = guia.IdDetalleGuiaServicio;
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(idguia), System.Text.Encoding.UTF8, "application/json") };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("CreateDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailMaterial(DetalleGuiaServicioMaterialModel guiam)
        {

            DetalleGuiaServiciosMaterial detalleGuiaServiciosMaterial = new DetalleGuiaServiciosMaterial();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosMaterial.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosMaterial.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosMaterial.IdMaterial = guiam.IdMaterial;
                    detalleGuiaServiciosMaterial.Cantidad = guiam.Cantidad;
                    db.DetalleGuiaServiciosMaterial.Add(detalleGuiaServiciosMaterial);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("CreateDetailRefection")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailRefection(DetalleGuiaServiciosRefaccion guiam)
        {

            DetalleGuiaServiciosRefaccion detalleGuiaServiciosRefaccion = new DetalleGuiaServiciosRefaccion();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosRefaccion.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosRefaccion.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosRefaccion.IdRefaccion = guiam.IdRefaccion;
                    detalleGuiaServiciosRefaccion.Cantidad = guiam.Cantidad;
                    db.DetalleGuiaServiciosRefaccion.Add(detalleGuiaServiciosRefaccion);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("CreateDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailJob(DetalleGuiaServicioPuestoModel guiam)
        {

            DetalleGuiaServiciosPuesto detalleGuiaServiciosPuesto = new DetalleGuiaServiciosPuesto();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosPuesto.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosPuesto.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosPuesto.IdPuesto = guiam.IdPuesto;
                    detalleGuiaServiciosPuesto.Cantidad = guiam.Cantidad;
                    db.DetalleGuiaServiciosPuesto.Add(detalleGuiaServiciosPuesto);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("EditHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditHeader(GuiaServicioModel guiam)
        {

            GuiaServicio guiaServicio = new GuiaServicio();
            try
            {

                if (guiam != null)
                {
                    guiaServicio = db.GuiaServicio.Where(x => x.IdGuiaServicio == guiam.IdGuiaServicio).FirstOrDefault();
                    guiaServicio.IdArea = guiam.IdArea;
                    guiaServicio.IdMarca = guiam.IdMarca;
                    guiaServicio.IdModelo = guiam.IdModelo;
                    guiaServicio.Km_Hr = guiam.Km_Hr;
                    guiaServicio.Descripcion = guiam.Descripcion;
                    db.SaveChanges(); ;

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("EditDetail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetail(DetalleGuiaServicioModel guiam)
        {

            DetalleGuiaServicio detalleGuia = new DetalleGuiaServicio();
            try
            {

                if (guiam != null)
                {
                    detalleGuia = db.DetalleGuiaServicio.Where(x => x.IdDetalleGuiaServicio == guiam.IdDetalleGuiaServicio).FirstOrDefault();
                    detalleGuia.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuia.Actividad = guiam.Actividad;
                    detalleGuia.Tiempo = guiam.Tiempo;
                    detalleGuia.Observaciones = guiam.Observaciones;
                    db.SaveChanges();

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("EditDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailMaterial(DetalleGuiaServicioMaterialModel guiam)
        {
            DetalleGuiaServiciosMaterial detalleGuiaServiciosMaterial = new DetalleGuiaServiciosMaterial();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosMaterial = db.DetalleGuiaServiciosMaterial.Where(x => x.IdDetalleGuiaServiciosMaterial == guiam.IdDetalleGuiaServiciosMaterial).FirstOrDefault();
                    detalleGuiaServiciosMaterial.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosMaterial.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosMaterial.IdMaterial = guiam.IdMaterial;
                    detalleGuiaServiciosMaterial.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("EditDetailRefection")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailRefection(DetalleGuiaServiciosRefaccion guiam)
        {

            DetalleGuiaServiciosRefaccion detalleGuiaServiciosRefaccion = new DetalleGuiaServiciosRefaccion();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosRefaccion = db.DetalleGuiaServiciosRefaccion.Where(x => x.IdDetalleGuiaServiciosRefaccion == guiam.IdDetalleGuiaServiciosRefaccion).FirstOrDefault();
                    detalleGuiaServiciosRefaccion.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosRefaccion.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosRefaccion.IdRefaccion = guiam.IdRefaccion;
                    detalleGuiaServiciosRefaccion.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("EditDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailJob(DetalleGuiaServicioPuestoModel guiam)
        {

            DetalleGuiaServiciosPuesto detalleGuiaServiciosPuesto = new DetalleGuiaServiciosPuesto();
            try
            {
                if (guiam != null)
                {
                    detalleGuiaServiciosPuesto = db.DetalleGuiaServiciosPuesto.Where(x => x.IdDetalleGuiaServiciosPuesto == guiam.IdDetalleGuiaServiciosPuesto).FirstOrDefault();
                    detalleGuiaServiciosPuesto.IdGuiaServicio = guiam.IdGuiaServicio;
                    detalleGuiaServiciosPuesto.IdDetalleGuiaServicio = guiam.IdDetalleGuiaServicio;
                    detalleGuiaServiciosPuesto.IdPuesto = guiam.IdPuesto;
                    detalleGuiaServiciosPuesto.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("DeleteHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteHeader(IdModelGuiaServicioModel id)
        {

            GuiaServicio guia = new GuiaServicio();
            try
            {
                if (id != null)
                {
                    guia = db.GuiaServicio.Where(x => x.IdGuiaServicio == id.IdGuiaServicio).FirstOrDefault();
                    guia.Activo_Inactivo = false;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("DeleteDetail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetail(IdModelGuiaServicioModelDetails id)
        {

            DetalleGuiaServicio guia = new DetalleGuiaServicio();
            try
            {
                if (id != null)
                {
                    guia = db.DetalleGuiaServicio.Where(x => x.IdDetalleGuiaServicio == id.IdDetalleGuiaServicio).FirstOrDefault();
                    if (guia != null)
                        db.DetalleGuiaServicio.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("DeleteDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailMaterial(IdModelGuiaServicioModelDetails id)
        {

            DetalleGuiaServiciosMaterial guia = new DetalleGuiaServiciosMaterial();
            try
            {
                if (id != null)
                {
                    guia = db.DetalleGuiaServiciosMaterial.Where(x => x.IdDetalleGuiaServiciosMaterial == id.IdGuiaServicioDetailMaterial).FirstOrDefault();
                    if (guia != null)
                        db.DetalleGuiaServiciosMaterial.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }

        [System.Web.Http.Route("DeleteDetailRefaction")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailRefaction(IdModelGuiaServicioModelDetails id)
        {

            DetalleGuiaServiciosRefaccion guia = new DetalleGuiaServiciosRefaccion();
            try
            {
                if (id != null)
                {
                    guia = db.DetalleGuiaServiciosRefaccion.Where(x => x.IdDetalleGuiaServiciosRefaccion == id.IdGuiaServicioDetailRefaction).FirstOrDefault();
                    if (guia != null)
                        db.DetalleGuiaServiciosRefaccion.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
            }

        }
        [System.Web.Http.Route("DeleteDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailJob(IdModelGuiaServicioModelDetails id)
        {

            DetalleGuiaServiciosPuesto guia = new DetalleGuiaServiciosPuesto();
            try
            {
                if (id != null)
                {
                    guia = db.DetalleGuiaServiciosPuesto.Where(x => x.IdDetalleGuiaServiciosPuesto == id.IdGuiaServicioDetailJob).FirstOrDefault();
                    if (guia != null)
                        db.DetalleGuiaServiciosPuesto.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Guia de Servicio");
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
        public class IdModelGuiaServicioModel
        {
            [DataMember]
            public int IdGuiaServicio { get; set; }
        }

        [DataContract]
        public class IdModelGuiaServicioModelDetails
        {
            [DataMember]
            public int IdDetalleGuiaServicio { get; set; }
            [DataMember]
            public int IdGuiaServicioDetailMaterial { get; set; }
            [DataMember]
            public int IdGuiaServicioDetailJob { get; set; }
            [DataMember]
            public int IdGuiaServicioDetailRefaction { get; set; }
           
        }
    }

}
