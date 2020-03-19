using System; using Newtonsoft.Json;
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

namespace core.Controllers
{
    [RoutePrefix("FastMan/Refacciones")]
    public class RefaccionesController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Refaccion> _refacciones;
        private Refaccion _refaccion;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetRefaccion()
        {
            try
            {
                _refacciones = await db.Refaccion.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_refacciones), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetRefaccion(IdModelRefaccion id)
        {
            try
            {
                _refaccion = await db.Refaccion.FindAsync(id.IdReFaccion);
                if (_refaccion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_refaccion), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutRefaccion(Refaccion Refaccion)
        {
            try
            {
                _refaccion = await db.Refaccion.FindAsync(Refaccion.IdRefaccion);
                if (_refaccion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _refaccion.IdSistemaRefacciones = Refaccion.IdSistemaRefacciones;
                    _refaccion.IdUnidadMedida = Refaccion.IdUnidadMedida;
                    _refaccion.NumeroParteRefaccion = Refaccion.NumeroParteRefaccion;
                    _refaccion.Nombre = Refaccion.Nombre;
                    _refaccion.Marca = Refaccion.Marca;
                    _refaccion.Posicion = Refaccion.Posicion;
                    _refaccion.CostoUnidad = Refaccion.CostoUnidad;
                    _refaccion.Cantidad = Refaccion.Cantidad;
                    _refaccion.Activo_Inactivo = Refaccion.Activo_Inactivo;
                    
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
        public async Task<HttpResponseMessage> PostRefaccion(Refaccion Refaccion)
        {
            try
            {
                Refaccion.Activo_Inactivo = true;
                db.Refaccion.Add(Refaccion);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteRefaccion(IdModelRefaccion id)
        {
            try
            {
                _refaccion = await db.Refaccion.FindAsync(id.IdReFaccion);
                if (_refaccion == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _refaccion.Activo_Inactivo = false;
                    db.Entry(_refaccion).State = EntityState.Modified;
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
                _refacciones = null;
                _refaccion = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelRefaccion
        {
            [DataMember]
            public string IdReFaccion { get; set; }
        }
    }
}