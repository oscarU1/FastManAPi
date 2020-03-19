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

namespace core.Controllers
{
    [RoutePrefix("FastMan/OrdenTrabajo")]
    public class OrdenTrabajoController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<OrdenTrabajo> _ordenesTrabajo;
        private OrdenTrabajo _ordenTrabajo;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetOrdenTrabajo()
        {
            try
            {
                _ordenesTrabajo = await db.OrdenTrabajo.Where(x => x.Activo_Inactivo == true && x.Unidad.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_ordenesTrabajo), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetOrdenTrabajo(IdModelOrdenTrabajo Id)
        {
            try
            {
                _ordenTrabajo = await db.OrdenTrabajo.FindAsync(Id.IdOrdenTrabajo);
                if (_ordenTrabajo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_ordenTrabajo), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutOrdenTrabajo(OrdenTrabajo OrdenTrabajo)
        {
            try
            {
                _ordenTrabajo = await db.OrdenTrabajo.FindAsync(OrdenTrabajo.IdOrdenTrabajo);
                if (_ordenTrabajo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {

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
        public async Task<HttpResponseMessage> PostOrdenTrabajo(OrdenTrabajo OrdenTrabajo)
        {
            try
            {
                OrdenTrabajo.Activo_Inactivo = true;
                db.OrdenTrabajo.Add(OrdenTrabajo);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteOrdenTrabajo(IdModelOrdenTrabajo Id)
        {
            try
            {
                _ordenTrabajo = await db.OrdenTrabajo.FindAsync(Id.IdOrdenTrabajo);
                if (_ordenTrabajo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _ordenTrabajo.Activo_Inactivo = false;
                    db.Entry(_ordenTrabajo).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Automatic")]
        public async Task<HttpResponseMessage> AutomiticGeneration(UnitInfo unit)
        {
            try
            {
                var currentUnit = await db.Unidad.FindAsync(unit.IdUnidad).ConfigureAwait(false);
                var currentServiceGuide = await db.GuiaServicio.FindAsync(unit.IdGuiaServicio).ConfigureAwait(false);

                //var unitubic = await db.UnidadUbicacion.FindAsync(unit.IdUnidad).ConfigureAwait(false);                
                var unitarea = await db.UnidadArea.FindAsync(unit.IdUnidad).ConfigureAwait(false);
                var unitproce = await db.UnidadProceso.FindAsync(unit.IdUnidad).ConfigureAwait(false);
                var unitsubp = await db.UnidadSubProceso.FindAsync(unit.IdUnidad).ConfigureAwait(false);




                var newWorkOrder = new OrdenTrabajo();

                newWorkOrder.Planeada = true;
                newWorkOrder.Activo_Inactivo = true;
                newWorkOrder.Ejecutada = false;
                newWorkOrder.FechaEjecuccion = DateTime.Now;
                newWorkOrder.FechaEjecucion = DateTime.Now;
                newWorkOrder.FechaGeneracion = DateTime.Now;
                if (unitarea != null)
                { newWorkOrder.IdArea = unitarea.IdArea; }
                if (unitproce != null)
                {
                    var proce = await db.Proceso.FindAsync(unitproce.IdProceso).ConfigureAwait(false);
                    newWorkOrder.IdArea = proce.IdArea;
                }
                if(unitsubp!= null)
                {
                    var subp = await db.SubProceso.FindAsync(unitsubp.IdSubProceso).ConfigureAwait(false);
                    var proce = await db.Proceso.FindAsync(subp.IdProceso).ConfigureAwait(false);
                    newWorkOrder.IdArea = proce.IdArea;
                }
                newWorkOrder.IdUnidad = currentUnit.IdUnidad;
                newWorkOrder.IdEstatusOrdenTrabajo = 1;
                

                //Guardar Orden de trabajo
                db.OrdenTrabajo.Add(newWorkOrder);
                var result = await db.SaveChangesAsync().ConfigureAwait(false);

                foreach (var detalleGuia in await db.DetalleGuiaServicio.Where(x => x.IdGuiaServicio == currentServiceGuide.IdGuiaServicio).ToListAsync().ConfigureAwait(false))
                {
                    int count = 0;
                    #region Guia de servicio
                    foreach (var actSer in await db.DetalleGuiaServiciosMaterial.Where(x => x.IdGuiaServicio == currentServiceGuide.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuia.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.DetalleGuiaServicio.Actividad,
                            Tiempo = 1,
                            IdMaterial = actSer.IdMaterial,
                            Descripcion = actSer.Material.Nombre,
                            Costo = actSer.Material.CostoUnidad,
                            IdTipoActividad = 2
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.DetalleGuiaServiciosRefaccion.Where(x => x.IdGuiaServicio == currentServiceGuide.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuia.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.DetalleGuiaServicio.Actividad,
                            Tiempo = 1,
                            IdRefaccion = actSer.IdRefaccion,
                            Descripcion = actSer.Refaccion.Nombre,
                            Costo = actSer.Refaccion.CostoUnidad,
                            IdTipoActividad = 1
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.DetalleGuiaServiciosPuesto.Where(x => x.IdGuiaServicio == currentServiceGuide.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuia.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.DetalleGuiaServicio.Actividad,
                            Tiempo = 1,
                            IdPuesto = actSer.IdPuesto,
                            Descripcion = actSer.Puesto.NombrePuesto,
                            Costo = actSer.Puesto.CostoHora.Value,
                            IdTipoActividad = 3

                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    if (count < 1)
                    {
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = detalleGuia.Actividad,
                            Tiempo = 1,
                            Costo = detalleGuia.Tiempo,
                            IdTipoActividad = 1
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    await db.SaveChangesAsync().ConfigureAwait(false);
                    #endregion
                }

                foreach (var backLog in await db.BackLog.Where(x => x.IdUnidad == unit.IdUnidad && x.Ejecutada.Value == false).ToListAsync().ConfigureAwait(false))
                {
                    int count = 0;
                    #region Backlog
                    foreach (var actSer in await db.BackLogMaterial.Where(x => x.BackLog.IdUnidad == unit.IdUnidad && x.BackLog.Ejecutada == false && x.IdBackLog == backLog.IdBackLog).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.BackLog.Actividad,
                            Tiempo = actSer.BackLog.Tiempo,
                            IdMaterial = actSer.IdMaterial,
                            Descripcion = actSer.Material.Nombre,
                            Costo = actSer.Material.CostoUnidad,
                            IdTipoActividad = 2
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.BackLogRefaccion.Where(x => x.BackLog.IdUnidad == unit.IdUnidad && x.BackLog.Ejecutada == false && x.IdBackLog == backLog.IdBackLog).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.BackLog.Actividad,
                            Tiempo = actSer.BackLog.Tiempo,
                            IdRefaccion = actSer.IdReFaccion,
                            Descripcion = actSer.Refaccion.Nombre,
                            Costo = actSer.Refaccion.CostoUnidad,
                            IdTipoActividad = 1
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.BackLogPuesto.Where(x => x.BackLog.IdUnidad == unit.IdUnidad && x.BackLog.Ejecutada == false && x.IdBackLog == backLog.IdBackLog).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.BackLog.Actividad,
                            Tiempo = actSer.BackLog.Tiempo,
                            IdPuesto = actSer.IdPuesto,
                            Descripcion = actSer.Puesto.NombrePuesto,
                            Costo = actSer.Puesto.CostoHora.Value,
                            IdTipoActividad = 3
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    if (count < 1)
                    {
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = backLog.Actividad,
                            Tiempo = backLog.Tiempo,
                            Costo = backLog.Tiempo,
                            IdTipoActividad = 1
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    await db.SaveChangesAsync().ConfigureAwait(false);
                    #endregion
                }

                foreach (var mttoPred in await db.MantenimientoPredictivo.Where(x => x.IdUnidad == unit.IdUnidad).ToListAsync().ConfigureAwait(false))
                {
                    int count = 0;
                    #region Mtto predictivo
                    foreach (var actSer in await db.MantenimientoPredictivoMaterial.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                            Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                            IdMaterial = actSer.IdMaterial,
                            Descripcion = actSer.Material.Nombre,
                            Costo = actSer.Material.CostoUnidad,
                            IdTipoActividad = 2
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.MantenimientoPredictivoRefaccion.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                            Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                            IdRefaccion = actSer.IdRefaccion,
                            Descripcion = actSer.Refaccion.Nombre,
                            Costo = actSer.Refaccion.CostoUnidad,
                            IdTipoActividad = 1
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    foreach (var actSer in await db.MantenimientoPredictivoPuesto.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                    {
                        count++;
                        var newMtto = new DetalleOrdenTrabajo()
                        {
                            IdOrdenTrabajo = newWorkOrder.IdOrdenTrabajo,
                            IdGuiaServicio = currentServiceGuide.IdGuiaServicio,
                            TipoServicio = currentServiceGuide.Descripcion,
                            Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                            Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                            IdPuesto = actSer.IdPuesto,
                            Descripcion = actSer.Puesto.NombrePuesto,
                            Costo = actSer.Puesto.CostoHora.Value,
                            IdTipoActividad = 3
                        };

                        db.DetalleOrdenTrabajo.Add(newMtto);
                    }

                    await db.SaveChangesAsync().ConfigureAwait(false);
                    #endregion
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("CreateActiVities")]
        public async Task<HttpResponseMessage> CreateActicities(List<ReqNewActivity> activitiesReq)
        {
            try
            {
                foreach (var newActivity in activitiesReq)
                {
                    switch (newActivity.Origen)
                    {
                        case 1:
                            var detalleGuiaServicio = await db.DetalleGuiaServicio.FindAsync(newActivity.IdActividad).ConfigureAwait(false);

                            int count = 0;
                            #region Guia de servicio
                            foreach (var actSer in await db.DetalleGuiaServiciosMaterial.Where(x => x.IdGuiaServicio == detalleGuiaServicio.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuiaServicio.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                            {
                                count++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdGuiaServicio = detalleGuiaServicio.IdGuiaServicio,
                                    TipoServicio = "GUIA SERVICIO",
                                    Actividad = actSer.DetalleGuiaServicio.Actividad,
                                    Tiempo = 1,
                                    IdMaterial = actSer.IdMaterial,
                                    Descripcion = actSer.Material.Nombre,
                                    Costo = actSer.Material.CostoUnidad,
                                    IdTipoActividad = 1
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.DetalleGuiaServiciosRefaccion.Where(x => x.IdGuiaServicio == detalleGuiaServicio.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuiaServicio.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                            {
                                count++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdGuiaServicio = detalleGuiaServicio.IdGuiaServicio,
                                    TipoServicio = "GUIA SERVICIO",
                                    Actividad = actSer.DetalleGuiaServicio.Actividad,
                                    Tiempo = 1,
                                    IdRefaccion = actSer.IdRefaccion,
                                    Descripcion = actSer.Refaccion.Nombre,
                                    Costo = actSer.Refaccion.CostoUnidad,
                                    IdTipoActividad = 1
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.DetalleGuiaServiciosPuesto.Where(x => x.IdGuiaServicio == detalleGuiaServicio.IdGuiaServicio && x.IdDetalleGuiaServicio == detalleGuiaServicio.IdDetalleGuiaServicio).ToListAsync().ConfigureAwait(false))
                            {
                                count++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdGuiaServicio = detalleGuiaServicio.IdGuiaServicio,
                                    TipoServicio = "GUIA SERVICIO",
                                    Actividad = actSer.DetalleGuiaServicio.Actividad,
                                    Tiempo = 1,
                                    IdPuesto = actSer.IdPuesto,
                                    Descripcion = actSer.Puesto.NombrePuesto,
                                    Costo = actSer.Puesto.CostoHora.Value,
                                    IdTipoActividad = 1

                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            if (count < 1)
                            {
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdGuiaServicio = detalleGuiaServicio.IdGuiaServicio,
                                    TipoServicio = "GUIA SERVICIO",
                                    Actividad = detalleGuiaServicio.Actividad,
                                    Tiempo = 1,
                                    Costo = detalleGuiaServicio.Tiempo,
                                    IdTipoActividad = 1
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            await db.SaveChangesAsync().ConfigureAwait(false);
                            #endregion

                            break;
                        case 2:
                            var detalleBacklog = await db.BackLog.FindAsync(newActivity.IdActividad).ConfigureAwait(false);

                            int count2 = 0;
                            #region Backlog
                            foreach (var actSer in await db.BackLogMaterial.Where(x => x.BackLog.Ejecutada == false && x.IdBackLog == detalleBacklog.IdBackLog).ToListAsync().ConfigureAwait(false))
                            {
                                count2++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdBackLog = detalleBacklog.IdBackLog,
                                    TipoServicio = "BACKLOG",
                                    Actividad = actSer.BackLog.Actividad,
                                    Tiempo = actSer.BackLog.Tiempo,
                                    IdMaterial = actSer.IdMaterial,
                                    Descripcion = actSer.Material.Nombre,
                                    Costo = actSer.Material.CostoUnidad,
                                    IdTipoActividad = 2
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.BackLogRefaccion.Where(x => x.BackLog.Ejecutada == false && x.IdBackLog == detalleBacklog.IdBackLog).ToListAsync().ConfigureAwait(false))
                            {
                                count2++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdBackLog = detalleBacklog.IdBackLog,
                                    TipoServicio = "BACKLOG",
                                    Actividad = actSer.BackLog.Actividad,
                                    Tiempo = actSer.BackLog.Tiempo,
                                    IdRefaccion = actSer.IdReFaccion,
                                    Descripcion = actSer.Refaccion.Nombre,
                                    Costo = actSer.Refaccion.CostoUnidad,
                                    IdTipoActividad = 2
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.BackLogPuesto.Where(x => x.BackLog.Ejecutada == false && x.IdBackLog == detalleBacklog.IdBackLog).ToListAsync().ConfigureAwait(false))
                            {
                                count2++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdBackLog = detalleBacklog.IdBackLog,
                                    TipoServicio = "BACKLOG",
                                    Actividad = actSer.BackLog.Actividad,
                                    Tiempo = actSer.BackLog.Tiempo,
                                    IdPuesto = actSer.IdPuesto,
                                    Descripcion = actSer.Puesto.NombrePuesto,
                                    Costo = actSer.Puesto.CostoHora.Value,
                                    IdTipoActividad = 2
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            if (count2 < 1)
                            {
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdBackLog = detalleBacklog.IdBackLog,
                                    TipoServicio = "BACKLOG",
                                    Actividad = detalleBacklog.Actividad,
                                    Tiempo = detalleBacklog.Tiempo,
                                    Costo = detalleBacklog.Tiempo,
                                    IdTipoActividad = 2
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            await db.SaveChangesAsync().ConfigureAwait(false);
                            #endregion

                            break;
                        case 3:
                            var mttoPred = await db.ActividadMantenimientoPredictivo.FindAsync(newActivity.IdActividad).ConfigureAwait(false);

                            int count3 = 0;
                            #region Mtto predictivo
                            foreach (var actSer in await db.MantenimientoPredictivoMaterial.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                            {
                                count3++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdMantenimientoPredictivo = mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo,
                                    TipoServicio = "MANTENIMIENTO PREDICTIVO",
                                    Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                                    Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                                    IdMaterial = actSer.IdMaterial,
                                    Descripcion = actSer.Material.Nombre,
                                    Costo = actSer.Material.CostoUnidad,
                                    IdTipoActividad = 3
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.MantenimientoPredictivoRefaccion.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                            {
                                count3++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdMantenimientoPredictivo = mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo,
                                    TipoServicio = "MANTENIMIENTO PREDICTIVO",
                                    Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                                    Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                                    IdRefaccion = actSer.IdRefaccion,
                                    Descripcion = actSer.Refaccion.Nombre,
                                    Costo = actSer.Refaccion.CostoUnidad,
                                    IdTipoActividad = 3
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            foreach (var actSer in await db.MantenimientoPredictivoPuesto.Where(x => x.ActividadMantenimientoPredictivo.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo == mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo && x.ActividadMantenimientoPredictivo.Ejecutada == false).ToListAsync().ConfigureAwait(false))
                            {
                                count3++;
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdMantenimientoPredictivo = mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo,
                                    TipoServicio = "MANTENIMIENTO PREDICTIVO",
                                    Actividad = actSer.ActividadMantenimientoPredictivo.Descripcion,
                                    Tiempo = actSer.ActividadMantenimientoPredictivo.Tiempo,
                                    IdPuesto = actSer.IdPuesto,
                                    Descripcion = actSer.Puesto.NombrePuesto,
                                    Costo = actSer.Puesto.CostoHora.Value,
                                    IdTipoActividad = 3
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            if (count3 < 1)
                            {
                                var newMtto = new DetalleOrdenTrabajo()
                                {
                                    IdOrdenTrabajo = newActivity.IdOrdenTrabajo,
                                    IdBackLog = mttoPred.DetalleMantenimientoPredictivo.IdMantenimientoPredictivo,
                                    TipoServicio = "MANTENIMIENTO PREDICTIVO",
                                    Actividad = mttoPred.Descripcion,
                                    Tiempo = mttoPred.Tiempo,
                                    Costo = mttoPred.Tiempo,
                                    IdTipoActividad = 3
                                };

                                db.DetalleOrdenTrabajo.Add(newMtto);
                            }

                            await db.SaveChangesAsync().ConfigureAwait(false);
                            #endregion

                            break;
                    }
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("GetServiceGuides")]
        public async Task<HttpResponseMessage> GetServiceGuides(IdModelOrdenTrabajo ordenTrabajo)
        {
            try
            {
                var _lordenTrabajo = await db.OrdenTrabajo.Include(o => o.Unidad).ToListAsync().ConfigureAwait(false);
                var _ordenTrabajo = _lordenTrabajo.Where(x => x.IdOrdenTrabajo == ordenTrabajo.IdOrdenTrabajo).First();
                var _guiasServicio = await db.GuiaServicio.Where(x => x.IdMarca == _ordenTrabajo.Unidad.IdMarca && x.IdModelo == _ordenTrabajo.Unidad.IdModelo && x.IdArea == _ordenTrabajo.IdArea).ToListAsync().ConfigureAwait(false);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(_guiasServicio), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("GetBacklog")]
        public async Task<HttpResponseMessage> GetBacklog(IdModelOrdenTrabajo ordenTrabajo)
        {
            try
            {
                var _lordenTrabajo = await db.OrdenTrabajo.Include(o => o.Unidad).ToListAsync().ConfigureAwait(false);
                var _ordenTrabajo = _lordenTrabajo.Where(x => x.IdOrdenTrabajo == ordenTrabajo.IdOrdenTrabajo).First();
                db.Configuration.LazyLoadingEnabled = false;
                //var _Backlogs = await db.BackLog.Where(x => x.IdUnidad == _ordenTrabajo.Unidad.IdUnidad && x.IdArea == _ordenTrabajo.IdArea && x.Ejecutada == false).ToListAsync().ConfigureAwait(false);
                var _Backlogs = await db.BackLog.Where(x => x.IdUnidad == _ordenTrabajo.Unidad.IdUnidad && x.Ejecutada == false).ToListAsync().ConfigureAwait(false);

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(_Backlogs), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("GetDetailsWorkOrder")]
        public async Task<HttpResponseMessage> GetDetailsWorkOrder(IdModelOrdenTrabajo ordenTrabajo)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                var _lordenTrabajo = await db.DetalleOrdenTrabajo.Where(x=>x.IdOrdenTrabajo == ordenTrabajo.IdOrdenTrabajo).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(_lordenTrabajo), System.Text.Encoding.UTF8, "application/json") };
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
                _ordenesTrabajo = null;
                _ordenTrabajo = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelOrdenTrabajo
        {
            [DataMember]
            public int IdOrdenTrabajo { get; set; }
        }

        [DataContract]
        public class ReqNewActivity
        {
            [DataMember]
            public int IdOrdenTrabajo { get; set; }
            [DataMember]
            public int IdActividad { get; set; }
            [DataMember]
            public int Origen { get; set; }
        }

    }

    public class UnitInfo
    {
        public int IdUnidad { get; set; }
        public int IdGuiaServicio { get; set; }
        public int IdUbicacion { get; set; }
    }
}