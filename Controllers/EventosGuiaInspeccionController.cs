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
using System.Web.Http.Cors;

namespace core.Controllers
{
    [RoutePrefix("FastMan/EventosGuiaInspeccion")]
    public class EventosGuiaInspeccionController : ApiController
    {
        private DBConnection db;
        private List<EventosGuiaInspeccion> _eventosGuiasInspeccion;
        private EventosGuiaInspeccion _eventosGuiaInspeccion;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdModelGuiaInspeccionIndex id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _eventosGuiasInspeccion = await db.EventosGuiaInspeccion.Where(x => x.Activo_Inactivo == true && x.IdGuiaInspeccion == id.IdGuiaInspeccion).ToListAsync().ConfigureAwait(false);
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_eventosGuiasInspeccion), System.Text.Encoding.UTF8, "application/json") };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdModelEventosGuiaInspeccion id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _eventosGuiaInspeccion = await db.EventosGuiaInspeccion.FindAsync(id.IdEventosGuiaInspeccion);
                    if (_eventosGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_eventosGuiaInspeccion)) };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(EventoGuiaInspeccion EventosGuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _eventosGuiaInspeccion = await db.EventosGuiaInspeccion.FindAsync(EventosGuiaInspeccion.IdEventosGuiaInspeccion);
                    if (_eventosGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        DateTime initialDate = DateTime.ParseExact(_eventosGuiaInspeccion.FechaInicio.ToString("yyyy-MM-dd ") + EventosGuiaInspeccion.FechaInicio, "yyyy-MM-dd HH:mm", null);
                        DateTime finalDate = DateTime.ParseExact(_eventosGuiaInspeccion.FechaInicio.ToString("yyyy-MM-dd ") + EventosGuiaInspeccion.FechaFin, "yyyy-MM-dd HH:mm", null);

                        _eventosGuiaInspeccion.FechaInicio = initialDate;
                        _eventosGuiaInspeccion.FechaFin = finalDate;
                        _eventosGuiaInspeccion.IdCatalogoEvento = EventosGuiaInspeccion.IdCatalogoEvento;

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
        public async Task<HttpResponseMessage> PostEventosGuiaInspeccion(EventoGuiaInspeccion EventosGuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    var guiHeader = await db.GuiaInspeccion.FindAsync(EventosGuiaInspeccion.IdGuiaInspeccion).ConfigureAwait(false);
                    if (guiHeader != null)
                    {
                        DateTime initialDate = DateTime.ParseExact(guiHeader.FechaHora_GuiaInpeccion.ToString("yyyy-MM-dd ") + EventosGuiaInspeccion.FechaInicio, "yyyy-MM-dd HH:mm", null);
                        DateTime finalDate = DateTime.ParseExact(guiHeader.FechaHora_GuiaInpeccion.ToString("yyyy-MM-dd ") + EventosGuiaInspeccion.FechaFin, "yyyy-MM-dd HH:mm", null);

                        EventosGuiaInspeccion _eventosGuia = new EventosGuiaInspeccion();
                        _eventosGuia.Activo_Inactivo = true;
                        _eventosGuia.FechaFin = finalDate;
                        _eventosGuia.FechaInicio = initialDate;
                        _eventosGuia.IdCatalogoEvento = EventosGuiaInspeccion.IdCatalogoEvento;
                        _eventosGuia.IdGuiaInspeccion = EventosGuiaInspeccion.IdGuiaInspeccion;

                        EventosGuiaInspeccion.Activo_Inactivo = true;
                        db.EventosGuiaInspeccion.Add(_eventosGuia);
                        await db.SaveChangesAsync();
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("Guia de inspección no encontrada.") };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(IdModelEventosGuiaInspeccion id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _eventosGuiaInspeccion = await db.EventosGuiaInspeccion.FindAsync(id.IdEventosGuiaInspeccion);
                    if (_eventosGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        _eventosGuiaInspeccion.Activo_Inactivo = false;
                        db.Entry(_eventosGuiaInspeccion).State = EntityState.Modified;
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
        public class IdModelEventosGuiaInspeccion
        {
            [DataMember]
            public int IdEventosGuiaInspeccion { get; set; }
        }

        [DataContract]
        public class IdModelGuiaInspeccionIndex
        {
            [DataMember]
            public int IdGuiaInspeccion { get; set; }
        }

        [DataContract]
        public class EventoGuiaInspeccion
        {
            [DataMember]
            public int IdEventosGuiaInspeccion { get; set; }
            [DataMember]
            public int IdCatalogoEvento { get; set; }
            [DataMember]
            public int IdGuiaInspeccion { get; set; }
            [DataMember]
            public string FechaInicio { get; set; }
            [DataMember]
            public string FechaFin { get; set; }
            [DataMember]
            public Nullable<bool> Activo_Inactivo { get; set; }
        }
    }

}