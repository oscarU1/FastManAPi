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
    [RoutePrefix("FastMan/Puestos")]
    public class PuestosController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Puesto> _puestos;
        private Puesto _puesto;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetPuesto()
        {
            try
            {
                _puestos = await db.Puesto.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_puestos), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetPuesto(IdModelPuesto id)
        {
            try
            {
                _puesto = await db.Puesto.FindAsync(id.IdPuesto);
                if (_puesto == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_puesto), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutPuesto(Puesto Puesto)
        {
            try
            {
                _puesto = await db.Puesto.FindAsync(Puesto.IdPuesto);
                if (_puesto == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _puesto.IdUbicacion = Puesto.IdUbicacion;
                    _puesto.IdArea = Puesto.IdArea;
                    _puesto.NombrePuesto = Puesto.NombrePuesto;
                    _puesto.Descripcion = Puesto.Descripcion;
                    _puesto.CostoHora = Puesto.CostoHora;
                    _puesto.Salario = Puesto.Salario;
                    _puesto.HorasMes = Puesto.HorasMes;
                    _puesto.Activo_Inactivo = Puesto.Activo_Inactivo;

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
        public async Task<HttpResponseMessage> PostPuesto(Puesto Puesto)
        {
            try
            {
                Puesto.Activo_Inactivo = true;
                db.Puesto.Add(Puesto);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeletePuesto(IdModelPuesto id)
        {
            try
            {
                _puesto = await db.Puesto.FindAsync(id.IdPuesto);
                if (_puesto == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _puesto.Activo_Inactivo = false;
                    db.Entry(_puesto).State = EntityState.Modified;
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
                _puestos = null;
                _puesto = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelPuesto
        {
            [DataMember]
            public string IdPuesto { get; set; }
        }
    }
}