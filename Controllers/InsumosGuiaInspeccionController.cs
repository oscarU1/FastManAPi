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
    [RoutePrefix("FastMan/InsumosGuiaInspeccion")]
    public class InsumosGuiaInspeccionController : ApiController
    {
        private DBConnection db;
        private List<InsumosGuiaInspeccion> _insumosGuiasInspeccion;
        private InsumosGuiaInspeccion _insumoGuiaInspeccion;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetInsumosGuiaInspeccion(InsumosGuiaInspeccion id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _insumosGuiasInspeccion = await db.InsumosGuiaInspeccion.Where(x => x.Activo_Inactivo == true && x.IdGuiaInspeccion == id.IdGuiaInspeccion).ToListAsync().ConfigureAwait(false);
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_insumosGuiasInspeccion), System.Text.Encoding.UTF8, "application/json") };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetInsumoGuiaInspeccion(InsumosGuiaInspeccion id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _insumoGuiaInspeccion = await db.InsumosGuiaInspeccion.FindAsync(id.IdInsumosGuiaInspeccion).ConfigureAwait(false);
                    if (_insumoGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_insumoGuiaInspeccion)) };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                }
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(InsumosGuiaInspeccion InsumoGuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _insumoGuiaInspeccion = await db.InsumosGuiaInspeccion.FindAsync(InsumoGuiaInspeccion.IdInsumosGuiaInspeccion);
                    if (_insumoGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        _insumoGuiaInspeccion.IdMaterial = InsumoGuiaInspeccion.IdMaterial;
                        _insumoGuiaInspeccion.Cantidad = InsumoGuiaInspeccion.Cantidad;
                        _insumoGuiaInspeccion.KM_HR = InsumoGuiaInspeccion.KM_HR;

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
        public async Task<HttpResponseMessage> PostEventosGuiaInspeccion(InsumosGuiaInspeccion InsumoGuiaInspeccion)
        {
            using (db = new DBConnection())
            {
                try
                {
                    var guiHeader = await db.GuiaInspeccion.FindAsync(InsumoGuiaInspeccion.IdGuiaInspeccion).ConfigureAwait(false);
                    if (guiHeader != null)
                    {
                        InsumosGuiaInspeccion _insumoGuia = new InsumosGuiaInspeccion();
                        _insumoGuia.Activo_Inactivo = true;
                        _insumoGuia.IdGuiaInspeccion = InsumoGuiaInspeccion.IdGuiaInspeccion;
                        _insumoGuia.IdMaterial = InsumoGuiaInspeccion.IdMaterial;
                        _insumoGuia.Cantidad = InsumoGuiaInspeccion.Cantidad;
                        _insumoGuia.KM_HR = InsumoGuiaInspeccion.KM_HR;
                        db.InsumosGuiaInspeccion.Add(_insumoGuia);
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
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(InsumosGuiaInspeccion id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    _insumoGuiaInspeccion = await db.InsumosGuiaInspeccion.FindAsync(id.IdInsumosGuiaInspeccion);
                    if (_insumoGuiaInspeccion == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        _insumoGuiaInspeccion.Activo_Inactivo = false;
                        db.Entry(_insumoGuiaInspeccion).State = EntityState.Modified;
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
    }
}