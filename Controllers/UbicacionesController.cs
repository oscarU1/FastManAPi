using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using core.Models;

namespace core.Controllers
{
    [RoutePrefix("FastMan/Ubicaciones")]
    public class UbicacionsController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Ubicacion> _ubicaciones;
        private Ubicacion _ubicacion;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetUbicacion()
        {
            try
            {
                _ubicaciones = await db.Ubicacion.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_ubicaciones), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetUbicacion(IdModelUbicacion id)
        {
            try
            {
                _ubicacion = await db.Ubicacion.FindAsync(id.IdUbicacion);
                if (_ubicacion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_ubicacion), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutUbicacion(Ubicacion Ubicacion)
        {
            try
            {
                _ubicacion = await db.Ubicacion.FindAsync(Ubicacion.IdUbicacion);
                if (_ubicacion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _ubicacion.IdEmpresa = Ubicacion.IdEmpresa;
                    _ubicacion.Calle = Ubicacion.Calle;
                    _ubicacion.NoExterior = Ubicacion.NoExterior;
                    _ubicacion.NoInterior = Ubicacion.NoInterior;
                    _ubicacion.CodPostal = Ubicacion.CodPostal;
                    _ubicacion.Colonia = Ubicacion.Colonia;
                    _ubicacion.Localidad = Ubicacion.Localidad;
                    _ubicacion.Municipio = Ubicacion.Municipio;
                    _ubicacion.Estado = Ubicacion.Estado;
                    _ubicacion.Pais = Ubicacion.Pais;
                    _ubicacion.Telefono = Ubicacion.Telefono;
                    _ubicacion.Gerente = Ubicacion.Gerente;
                    Ubicacion.Activo_Inactivo = true;
                    _ubicacion.Activo_Inactivo = Ubicacion.Activo_Inactivo;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Create")]
        public async Task<HttpResponseMessage> PostUbicacion(UbicacionModel Ubicacion)
        {
            try
            {
                if (Ubicacion != null)
                {
                    _ubicacion = new Ubicacion();
                    _ubicacion.IdEmpresa = Ubicacion.IdEmpresa.Value;
                    _ubicacion.Calle = Ubicacion.Calle;
                    _ubicacion.NoExterior = Ubicacion.NoExterior;
                    _ubicacion.NoInterior = Ubicacion.NoInterior;
                    _ubicacion.CodPostal = Ubicacion.CodPostal.Value;
                    _ubicacion.Colonia = Ubicacion.Colonia;
                    _ubicacion.Localidad = Ubicacion.Localidad;
                    _ubicacion.Municipio = Ubicacion.Municipio;
                    _ubicacion.Estado = Ubicacion.Estado;
                    _ubicacion.Pais = Ubicacion.Pais;
                    _ubicacion.Telefono = Ubicacion.Telefono;
                    _ubicacion.Gerente = Ubicacion.Gerente.Value;
                    Ubicacion.Activo_Inactivo = true;
                    _ubicacion.Activo_Inactivo = Ubicacion.Activo_Inactivo;
                    db.Ubicacion.Add(_ubicacion);
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.NotAcceptable);

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteUbicacion(IdModelUbicacion id)
        {
            try
            {
                _ubicacion = await db.Ubicacion.FindAsync(id.IdUbicacion);
                if (_ubicacion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _ubicacion.Activo_Inactivo = false;
                    db.Entry(_ubicacion).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
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
                _ubicaciones = null;
                _ubicacion = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelUbicacion
        {
            [DataMember]
            public int IdUbicacion { get; set; }
        }
    }
}