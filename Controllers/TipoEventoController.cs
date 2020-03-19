using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using core.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace core.Controllers
{
    [RoutePrefix("FastMan/TipoEvento")]
    public class TipoEventoController : ApiController
    {
        private DBConnection db = new DBConnection();

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetCatalogoEvento()
        {
            try
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(await db.TipoEvento.ToListAsync().ConfigureAwait(false)), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdTipoEventoModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    TipoEvento tipo = new TipoEvento();
                    tipo = await db.TipoEvento.FindAsync(id.IdTipoEvento);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(tipo), System.Text.Encoding.UTF8, "application/json") };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(TipoEventosModel evento)
        {
            using (db = new DBConnection())
            {
                try
                {
                    TipoEvento tipo = new TipoEvento();
                    tipo = await db.TipoEvento.Where(x => x.IdTipoEvento == evento.IdTipoEvento).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        tipo.IdArea = evento.IdArea;
                        tipo.Drescripcion = evento.Drescripcion;
                        await db.SaveChangesAsync();
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Create")]
        public async Task<HttpResponseMessage> PostEventosGuiaInspeccion(TipoEventosModel tipoEventos)
        {
            using (db = new DBConnection())
            {
                try
                {
                    TipoEvento tipo = new TipoEvento();
                    tipo.IdArea = tipoEventos.IdArea;
                    tipo.Drescripcion = tipoEventos.Drescripcion;
                    db.TipoEvento.Add(tipo);
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);

                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(IdTipoEventoModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    TipoEvento tipo = new TipoEvento();
                    tipo = await db.TipoEvento.FindAsync(id.IdTipoEvento);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        db.TipoEvento.Remove(tipo);                        
                        await db.SaveChangesAsync();
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }


        [DataContract]
        public class IdTipoEventoModel
        {
            [DataMember]
            public int IdTipoEvento { get; set; }
        }

        [DataContract]
        public class TipoEventosModel
        {
            [DataMember]
            public int IdTipoEvento { get; set; }
            [DataMember]
            public int IdArea { get; set; }
            [DataMember]
            public string Drescripcion { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}