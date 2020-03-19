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
    [RoutePrefix("FastMan/GuiaInspeccion")]
    public class GuiaInspeccionController : ApiController
    {
        private DBConnection db;
        private List<GuiaInspeccion> _guiasInspeccion;
        private GuiaInspeccion _guiaInspeccion;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetGuiaInspeccion()
        {
            using (db = new DBConnection())
            {
                try
                {
                    _guiasInspeccion = await db.GuiaInspeccion.Where(x => x.Activo_Inactivo == true && x.Unidad.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_guiasInspeccion), System.Text.Encoding.UTF8, "application/json") };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetGuiaInspeccion(IdModelGuiaInspeccion Id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _guiaInspeccion = await db.GuiaInspeccion.FindAsync(Id.IdGuiaInspeccion);
                    if (_guiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_guiaInspeccion), System.Text.Encoding.UTF8, "application/json") };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutGuiaInspeccion(GuiaInspeccion GuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _guiaInspeccion = await db.GuiaInspeccion.FindAsync(GuiaInspeccion.IdGuiaInspeccion);
                    if (_guiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        _guiaInspeccion.FechaHora_GuiaInpeccion = GuiaInspeccion.FechaHora_GuiaInpeccion;
                        _guiaInspeccion.HoraInicial = GuiaInspeccion.HoraInicial;
                        _guiaInspeccion.HoraFinal = GuiaInspeccion.HoraFinal;
                        _guiaInspeccion.IdTrabajador = GuiaInspeccion.IdTrabajador;
                        _guiaInspeccion.IdTurno = GuiaInspeccion.IdTurno;
                        _guiaInspeccion.IdArea = GuiaInspeccion.IdArea;
                        _guiaInspeccion.IdUnidad = GuiaInspeccion.IdUnidad;
                        _guiaInspeccion.KilometrajeFinal = GuiaInspeccion.KilometrajeFinal;
                        _guiaInspeccion.KilometrajeInicial = GuiaInspeccion.KilometrajeInicial;
                        _guiaInspeccion.Observaciones = GuiaInspeccion.Observaciones;

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
        public async Task<HttpResponseMessage> PostGuiaInspeccion(GuiaInspeccion GuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    GuiaInspeccion.Activo_Inactivo = true;
                    db.GuiaInspeccion.Add(GuiaInspeccion);
                    await db.SaveChangesAsync().ConfigureAwait(false);

                    var unit = await db.Unidad.FindAsync(GuiaInspeccion.IdUnidad).ConfigureAwait(false);

                    if (db.OrdenTrabajo.Where(x => x.IdUnidad == unit.IdUnidad).Count() > 0)
                    {
                        var lastOrder = db.OrdenTrabajo.Where(x => x.IdUnidad == unit.IdUnidad).Max(x => x.IdOrdenTrabajo);
                        var lastServ = await db.OrdenTrabajo.FindAsync(lastOrder).ConfigureAwait(false);
                        var listtIdSer = await db.GuiaServicio.Where(x=>x.IdMarca== unit.IdMarca && x.IdModelo == unit.IdModelo).OrderBy(x => x.Km_Hr).ToListAsync().ConfigureAwait(false);

//                        var listtIdSer = await db.GuiaServicio.Where(x => x.IdArea == unit.IdArea && unit.IdMarca == x.IdMarca && x.IdModelo == unit.IdModelo).OrderBy(x => x.Km_Hr).ToListAsync().ConfigureAwait(false);


                        if (!unit.Km_Hr.Value)
                        {
                            //Kilometros
                            var nextSer = listtIdSer.Where(x => x.Km_Hr <= GuiaInspeccion.KilometrajeFinal).ToList().FirstOrDefault();
                            if (nextSer != null)
                            {
                                var dateini = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Min(x => x.FechaHora_GuiaInpeccion.Date);
                                var datefin = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Max(x => x.FechaHora_GuiaInpeccion.Date);
                                var dias = (datefin - dateini).TotalDays;
                                var prom = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Sum(x => x.KilometrajeFinal) / dias;

                                var diasRestantes = (nextSer.Km_Hr - GuiaInspeccion.KilometrajeFinal) / prom;
                                if (diasRestantes <= 30)
                                {
                                    if (!lastServ.DetalleOrdenTrabajo.ToList().Exists(x => x.IdGuiaServicio == nextSer.IdGuiaServicio))
                                    {
                                        var response = await new OrdenTrabajoController().AutomiticGeneration(new UnitInfo() {IdGuiaServicio = nextSer.IdGuiaServicio, IdUnidad = unit.IdUnidad }).ConfigureAwait(false);
                                        if (response.StatusCode == HttpStatusCode.OK)
                                        {
                                            //Mandar Notificacion
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            //Horas
                            //Kilometros
                            var nextSer = listtIdSer.Where(x => GuiaInspeccion.HoraFinal <= x.Km_Hr).ToList().FirstOrDefault();
                            if (nextSer != null)
                            {
                                var dateini = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Min(x => x.FechaHora_GuiaInpeccion.Date);
                                var datefin = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Max(x => x.FechaHora_GuiaInpeccion.Date);
                                var dias = (datefin - dateini).TotalDays;
                                var prom = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Sum(x => x.HoraFinal) / dias;

                                var diasRestantes = (nextSer.Km_Hr - GuiaInspeccion.HoraFinal) / prom;
                                if (diasRestantes <= 30)
                                {
                                    if (!lastServ.DetalleOrdenTrabajo.ToList().Exists(x => x.IdGuiaServicio == nextSer.IdGuiaServicio))
                                    {
                                        var response = await new OrdenTrabajoController().AutomiticGeneration(new UnitInfo() {IdGuiaServicio = nextSer.IdGuiaServicio, IdUnidad = unit.IdUnidad }).ConfigureAwait(false);
                                        if (response.StatusCode == HttpStatusCode.OK)
                                        {
                                            //Mandar Notificacion
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        var listtIdSer = await db.GuiaServicio.Where(x => x.IdMarca == unit.IdMarca && x.IdModelo == unit.IdModelo).OrderBy(x => x.Km_Hr).ToListAsync().ConfigureAwait(false);

                        if (!unit.Km_Hr.Value)
                        {
                            //Kilometros
                            var nextSer = listtIdSer.Where(x => x.Km_Hr <= GuiaInspeccion.KilometrajeFinal).ToList().FirstOrDefault();
                            if (nextSer != null)
                            {
                                var dateini = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Min(x => x.FechaHora_GuiaInpeccion.Date);
                                var datefin = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Max(x => x.FechaHora_GuiaInpeccion.Date);
                                var dias = (datefin - dateini).TotalDays;
                                var prom = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Sum(x => x.KilometrajeFinal) / dias;

                                var diasRestantes = (nextSer.Km_Hr - GuiaInspeccion.KilometrajeFinal) / prom;
                                if (diasRestantes <= 30)
                                {
                                    var response = await new OrdenTrabajoController().AutomiticGeneration(new UnitInfo() {IdGuiaServicio = nextSer.IdGuiaServicio, IdUnidad = unit.IdUnidad }).ConfigureAwait(false);
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        //Mandar Notificacion
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Horas
                            //Kilometros
                            var nextSer = listtIdSer.Where(x => GuiaInspeccion.HoraFinal <= x.Km_Hr).ToList().FirstOrDefault();
                            if (nextSer != null)
                            {
                                var dateini = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).OrderBy(x => x.FechaHora_GuiaInpeccion).Select(x => x.FechaHora_GuiaInpeccion).FirstOrDefault();
                                var datefin = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).OrderByDescending(x => x.FechaHora_GuiaInpeccion).Select(x => x.FechaHora_GuiaInpeccion).FirstOrDefault();
                                var dias = (datefin - dateini).TotalDays;
                                var prom = db.GuiaInspeccion.Where(x => x.IdUnidad == unit.IdUnidad).Sum(x => x.HoraFinal) / dias;

                                var diasRestantes = (nextSer.Km_Hr - GuiaInspeccion.HoraFinal) / prom;
                                if (diasRestantes <= 30)
                                {
                                    var response = await new OrdenTrabajoController().AutomiticGeneration(new UnitInfo() { IdGuiaServicio = nextSer.IdGuiaServicio, IdUnidad = unit.IdUnidad }).ConfigureAwait(false);
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        //Mandar Notificacion
                                    }
                                }

                            }
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteGuiaInspeccion(IdModelGuiaInspeccion Id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _guiaInspeccion = await db.GuiaInspeccion.FindAsync(Id.IdGuiaInspeccion);
                    if (_guiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        _guiaInspeccion.Activo_Inactivo = false;
                        db.Entry(_guiaInspeccion).State = EntityState.Modified;
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
        public class IdModelGuiaInspeccion
        {
            [DataMember]
            public int IdGuiaInspeccion { get; set; }
        }
    }
}