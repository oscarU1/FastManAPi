using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using core.Models;
using Newtonsoft.Json;

namespace apifastman.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("FastMan/Trabajador")]
    public class TrabajadorController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Trabajador> acu = new List<Trabajador>();
            try
            {
                acu = db.Trabajador.Where(x=> x.Activo_Inactivo==true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };


            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Trabajador");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelTrabajador Id)
        {
            Trabajador acu = new Trabajador();
            try
            {
                if (Id != null)
                { acu = db.Trabajador.Where(x => x.IdTrabajador == Id.IdTrabajador).FirstOrDefault(); }
                if (acu != null)
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Trabajador");
            }

        }
        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Edit(TrabajadorModel trabajador)
        {
            Trabajador acu = new Trabajador();
            try
            {
                if (trabajador != null)
                {
                    acu = db.Trabajador.Where(x => x.IdTrabajador == trabajador.IdTrabajador).FirstOrDefault();
                    if (acu != null)
                    {
                        acu.IdTrabajador = trabajador.IdTrabajador;
                        acu.IdPuesto = trabajador.IdPuesto;
                        acu.Nombres = trabajador.Nombres;
                        acu.Apellidos = trabajador.Apellidos;
                        acu.Telefono = trabajador.Telefono;
                        acu.Calle = trabajador.Calle;
                        acu.NroExterior = trabajador.NroExterior;
                        acu.NroInterior = trabajador.NroInterior;
                        acu.Colonia = trabajador.Colonia;
                        acu.CodigoPostal = trabajador.CodigoPostal;
                        acu.Localidad = trabajador.Localidad;
                        acu.Municipio = trabajador.Municipio;
                        acu.Estado = trabajador.Estado;
                        acu.FechaIngreso = trabajador.FechaIngreso;
                        acu.NroSeguro = trabajador.NroSeguro;
                        acu.RFC = trabajador.RFC;
                        acu.NombreContactoEmergencia = trabajador.NombreContactoEmergencia;
                        acu.TelefonoContactoEmergencia = trabajador.TelefonoContactoEmergencia;
                        acu.Activo_Inactivo = trabajador.Activo_Inactivo;
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Trabajador");
            }

        }
        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(TrabajadorModel trabajador)
        {
            Trabajador acu = new Trabajador();
            try
            {
                if (trabajador != null)
                {
                    acu.IdPuesto = trabajador.IdPuesto;
                    acu.Nombres = trabajador.Nombres;
                    acu.Apellidos = trabajador.Apellidos;
                    acu.Telefono = trabajador.Telefono;
                    acu.Calle = trabajador.Calle;
                    acu.NroExterior = trabajador.NroExterior;
                    acu.NroInterior = trabajador.NroInterior;
                    acu.Colonia = trabajador.Colonia;
                    acu.CodigoPostal = trabajador.CodigoPostal;
                    acu.Localidad = trabajador.Localidad;
                    acu.Municipio = trabajador.Municipio;
                    acu.Estado = trabajador.Estado;
                    acu.FechaIngreso = trabajador.FechaIngreso;
                    acu.NroSeguro = trabajador.NroSeguro;
                    acu.RFC = trabajador.RFC;
                    acu.NombreContactoEmergencia = trabajador.NombreContactoEmergencia;
                    acu.TelefonoContactoEmergencia = trabajador.TelefonoContactoEmergencia;
                    acu.Activo_Inactivo = true;

                    db.Trabajador.Add(acu);
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "trabajador");
            }

        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(IdModelTrabajador Id)
        {
            Trabajador acu = new Trabajador();
            try
            {
                if (Id != null)
                {
                    acu = db.Trabajador.Where(x => x.IdTrabajador == Id.IdTrabajador).FirstOrDefault();
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Trabajador");
            }

        }

        [System.Web.Http.HttpPost, Route("AssingTrabajadorArea")]
        public async Task<HttpResponseMessage> AssingUnidadArea(AssingTrabajadorAreaModel assing)
        {
            try
            {
                TrabajadorArea acu = new TrabajadorArea();

                acu.IdArea = assing.IdArea;
                acu.IdTrabajador = assing.IdTrabajador;
                db.TrabajadorArea.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingTrabajadorProceso")]
        public async Task<HttpResponseMessage> AssingUnidadProceso(AssingTrabajadorProcesoModel assing)
        {
            try
            {
                TrabajadorProceso acu = new TrabajadorProceso();

                acu.IdProceso = assing.IdProceso;
                acu.IdTrabajador = assing.IdTrabajador;
                db.TrabajadorProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingTrabajadorSubProceso")]
        public async Task<HttpResponseMessage> AssingUnidadSubProceso(AssingTrabajadorSubProcesoModel assing)
        {
            try
            {
                TrabajadorSubProceso acu = new TrabajadorSubProceso();

                acu.IdSubProceso = assing.IdSubProceso;
                acu.IdTrabajador = assing.IdTrabajador;
                db.TrabajadorSubProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingTrabajadorUbicacion")]
        public async Task<HttpResponseMessage> AssingUnidadUbicacion(AssingTrabajadorUbicacionModel assing)
        {
            try
            {
                TrabajadorUbicacion acu = new TrabajadorUbicacion();

                acu.IdUbicacion = assing.IdUbicacion;
                acu.IdTrabajador = assing.IdTrabajador;
                db.TrabajadorUbicacion.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
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
        public class IdModelTrabajador
        {
            [DataMember]
            public int IdTrabajador { get; set; }
        }

        public class AssingTrabajadorAreaModel
        {
            public int IdArea { get; set; }
            public int IdTrabajador { get; set; }
        }
        public class AssingTrabajadorProcesoModel
        {
            public int IdProceso { get; set; }
            public int IdTrabajador { get; set; }
        }
        public class AssingTrabajadorSubProcesoModel
        {
            public int IdSubProceso { get; set; }
            public int IdTrabajador { get; set; }
        }
        public class AssingTrabajadorUbicacionModel
        {
            public int IdUbicacion { get; set; }
            public int IdTrabajador { get; set; }
        }
    }

}
