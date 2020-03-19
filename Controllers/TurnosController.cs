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
    [RoutePrefix("FastMan/Turnos")]
    public class TurnosController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Turno> _turnos;
        private Turno _turno;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetTurno()
        {
            try
            {
                _turnos = await db.Turno.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_turnos), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetTurno(IdModelTurno Id)
        {
            try
            {
                _turno = await db.Turno.FindAsync(Id.IdTurno);
                if (_turno == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_turno), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutTurno(Turno Turno)
        {
            try
            {
                _turno = await db.Turno.FindAsync(Turno.IdTurno);
                if (_turno == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _turno.IdArea = Turno.IdArea;
                    _turno.Descripcion = Turno.Descripcion;
                    _turno.HoraFin = Turno.HoraFin;
                    _turno.HoraInicio = Turno.HoraInicio;
                    _turno.Activo_Inactivo = Turno.Activo_Inactivo;
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
        public async Task<HttpResponseMessage> PostTurno(Turno Turno)
        {
            try
            {
                Turno.Activo_Inactivo = true;
                db.Turno.Add(Turno);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteTurno(IdModelTurno Id)
        {
            try
            {
                _turno = await db.Turno.FindAsync(Id.IdTurno);
                if (_turno == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _turno.Activo_Inactivo = false;
                    db.Entry(_turno).State = EntityState.Modified;
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
                _turnos = null;
                _turno = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelTurno
        {
            [DataMember]
            public int IdTurno { get; set; }
        }
    }
}