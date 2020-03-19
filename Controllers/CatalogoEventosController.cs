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
    [RoutePrefix("FastMan/Eventos")]
    public class CatalogoEventosController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<EventoService> _catalogoEventos = new List<EventoService>();
        private EventoService _catalogoEvento = new EventoService();

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetCatalogoEvento()
        {
            try
            {
                foreach (var evenServise in await db.CatalogoEvento.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false))
                {
                    _catalogoEventos.Add(new EventoService()
                    {
                        IdCatalogoEvento = evenServise.IdCatalogoEvento,
                        IdArea = evenServise.IdArea,
                        IdTipoEvento = evenServise.IdTipoEvento,
                        CodigoEvento = evenServise.CodigoEvento,
                        DescripcionEvento = evenServise.DescripcionEvento
                    });
                }
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_catalogoEventos), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetCatalogoEvento(IdModelCatalogoEventos Id)
        {
            try
            {
                var entry = await db.CatalogoEvento.FindAsync(Id.IdCatalogoEvento);
                if (entry == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _catalogoEvento = new EventoService()
                    {
                        IdCatalogoEvento = entry.IdCatalogoEvento,
                        IdArea = entry.IdArea,
                        IdTipoEvento = entry.IdTipoEvento,
                        CodigoEvento = entry.CodigoEvento,
                        DescripcionEvento = entry.DescripcionEvento
                    };
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_catalogoEvento), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutCatalogoEvento(EventoService CatalogoEvento)
        {
            try
            {
                var entry = await db.CatalogoEvento.FindAsync(CatalogoEvento.IdCatalogoEvento);
                if (_catalogoEvento == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    entry.IdArea = CatalogoEvento.IdArea;
                    entry.IdTipoEvento = CatalogoEvento.IdTipoEvento;
                    entry.CodigoEvento = CatalogoEvento.CodigoEvento;
                    entry.DescripcionEvento = CatalogoEvento.DescripcionEvento;

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
        public async Task<HttpResponseMessage> PostCatalogoEvento(EventoService CatalogoEvento)
        {
            try
            {
                db.CatalogoEvento.Add(new Models.CatalogoEvento()
                {
                    IdArea = CatalogoEvento.IdArea,
                    IdTipoEvento = CatalogoEvento.IdTipoEvento,
                    CodigoEvento = CatalogoEvento.CodigoEvento,
                    DescripcionEvento = CatalogoEvento.DescripcionEvento,
                    Activo_Inactivo = true,
                });
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteCatalogoEvento(IdModelCatalogoEventos Id)
        {
            try
            {
                var entry = await db.CatalogoEvento.FindAsync(Id.IdCatalogoEvento);
                if (entry == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    entry.Activo_Inactivo = false;
                    db.Entry(entry).State = EntityState.Modified;
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
                _catalogoEventos = null;
                _catalogoEvento = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelCatalogoEventos
        {
            [DataMember]
            public int IdCatalogoEvento { get; set; }
        }
    }


    public class EventoService
    {
        public int IdCatalogoEvento { get; set; }
        public int IdArea { get; set; }
        public int IdTipoEvento { get; set; }
        public int CodigoEvento { get; set; }
        public string DescripcionEvento { get; set; }
    }
}