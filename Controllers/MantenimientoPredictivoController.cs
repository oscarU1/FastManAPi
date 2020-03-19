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
    [RoutePrefix("FastMan/MantenimientoPredictivo")]
    public class MantenimientoPredictivoController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<MantenimientoPredictivo> _mantenimientosPredictivo;
        private MantenimientoPredictivo _mantenimientoPredictivo;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetMantenimientoPredictivo()
        {
            try
            {
                _mantenimientosPredictivo = await db.MantenimientoPredictivo.Where(x=>x.Unidad.Activo_Inactivo==true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_mantenimientosPredictivo), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetMantenimientoPredictivo(IdModelMantenimientoPredictivo Id)
        {
            try
            {
                _mantenimientoPredictivo = await db.MantenimientoPredictivo.FindAsync(Id.IdMantenimientoPredictivo);
                if (_mantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_mantenimientoPredictivo), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("EditHeader")]
        public async Task<HttpResponseMessage> PutMantenimientoPredictivo(MantenimientoPredictivo MantenimientoPredictivo)
        {
            try
            {
                _mantenimientoPredictivo = await db.MantenimientoPredictivo.FindAsync(MantenimientoPredictivo.IdMantenimientoPredictivo);
                if (_mantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _mantenimientoPredictivo.Fecha = MantenimientoPredictivo.Fecha;
                    _mantenimientoPredictivo.IdArea = MantenimientoPredictivo.IdArea;
                    _mantenimientoPredictivo.IdUnidad = MantenimientoPredictivo.IdUnidad;

                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
        [HttpPost, Route("EditDetail")]
        public async Task<HttpResponseMessage> PutMantenimientoPredictivoDetail(MantenimientoPredictivoDetailModel MantenimientoPredictivod)
        {
            try
            {
                DetalleMantenimientoPredictivo detalleMantenimientoPredictivo = new DetalleMantenimientoPredictivo();
                detalleMantenimientoPredictivo = db.DetalleMantenimientoPredictivo.Where(x => x.IdDetalleMantenimientoPredictivo == MantenimientoPredictivod.IdDetalleMantenimientoPredictivo).FirstOrDefault();
                if (detalleMantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    detalleMantenimientoPredictivo.Descripcion = MantenimientoPredictivod.Descripcion;
                    detalleMantenimientoPredictivo.Unidad = MantenimientoPredictivod.Unidad;
                    detalleMantenimientoPredictivo.LimiteInf = MantenimientoPredictivod.LimiteInf;
                    detalleMantenimientoPredictivo.LimiteSup = MantenimientoPredictivod.LimiteSup;                    
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("EditDetailActivity")]
        public async Task<HttpResponseMessage> PutMantenimientoPredictivoDetailActivity(ActividadMantenimientoPredictivoModel MantenimientoPredictivod)
        {
            try
            {
                ActividadMantenimientoPredictivo detalleMantenimientoPredictivo = new ActividadMantenimientoPredictivo();
                detalleMantenimientoPredictivo = db.ActividadMantenimientoPredictivo.Where(x => x.IdActividadMantenimientoPredictivo == MantenimientoPredictivod.IdActividadMantenimientoPredictivo).FirstOrDefault();
                if (detalleMantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    detalleMantenimientoPredictivo.IdDetalleMantenimientoPredictivo = MantenimientoPredictivod.IdDetalleMantenimientoPredictivo;
                    detalleMantenimientoPredictivo.Descripcion = MantenimientoPredictivod.Descripcion;
                    detalleMantenimientoPredictivo.Tiempo = MantenimientoPredictivod.Tiempo;
                    detalleMantenimientoPredictivo.Ejecutada = MantenimientoPredictivod.Ejecutada;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [System.Web.Http.Route("EditDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailMaterial(MantenimientoPredictivoMaterialModel guiam)
        {
            MantenimientoPredictivoMaterial mantenimientoPredictivo = new MantenimientoPredictivoMaterial();
            try
            {
                if (guiam != null)
                {
                    mantenimientoPredictivo = db.MantenimientoPredictivoMaterial.Where(x => x.IdMantenimientoPredictivoMaterial == guiam.IdMantenimientoPredictivoMaterial).FirstOrDefault();
                    mantenimientoPredictivo.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    mantenimientoPredictivo.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    mantenimientoPredictivo.IdMaterial = guiam.IdMaterial;
                    mantenimientoPredictivo.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("EditDetailRefection")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailRefection(MantenimientoPredictivoRefactionModel guiam)
        {

            MantenimientoPredictivoRefaccion mantenimientoPredictivo = new MantenimientoPredictivoRefaccion();
            try
            {
                if (guiam != null)
                {
                    mantenimientoPredictivo = db.MantenimientoPredictivoRefaccion.Where(x => x.IdMantenimientoPredictivoRefaccion == guiam.IdMantenimientoPredictivoRefaccion).FirstOrDefault();
                    mantenimientoPredictivo.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    mantenimientoPredictivo.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    mantenimientoPredictivo.IdRefaccion = guiam.IdReFaccion;
                    mantenimientoPredictivo.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("EditDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailJob(MantenimientoPredictivoJobModel guiam)
        {

            MantenimientoPredictivoPuesto backLogPuesto = new MantenimientoPredictivoPuesto();
            try
            {
                if (guiam != null)
                {
                    backLogPuesto = db.MantenimientoPredictivoPuesto.Where(x => x.IdMantenimientoPredictivoPuesto == guiam.IdMantenimientoPredictivoPuesto).FirstOrDefault();
                    backLogPuesto.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    backLogPuesto.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    backLogPuesto.IdPuesto = guiam.IdPuesto;
                    backLogPuesto.Cantidad = guiam.Cantidad;
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [HttpPost, Route("CreateHeader")]
        public async Task<HttpResponseMessage> PostMantenimientoPredictivo(MantenimientoPredictivo MantenimientoPredictivo)
        {
            try
            {                
                db.MantenimientoPredictivo.Add(MantenimientoPredictivo);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
        [HttpPost, Route("CreateDetail")]
        public async Task<HttpResponseMessage> PostMantenimientoPredictivoDetail(MantenimientoPredictivoDetailModel MantenimientoPredictivod)
        {
            try
            {
                DetalleMantenimientoPredictivo detalleMantenimientoPredictivo = new DetalleMantenimientoPredictivo();
                if (detalleMantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    detalleMantenimientoPredictivo.IdMantenimientoPredictivo = MantenimientoPredictivod.IdMantenimientoPredictivo;
                    detalleMantenimientoPredictivo.Descripcion = MantenimientoPredictivod.Descripcion;
                    detalleMantenimientoPredictivo.Unidad = MantenimientoPredictivod.Unidad;
                    detalleMantenimientoPredictivo.LimiteInf = MantenimientoPredictivod.LimiteInf;
                    detalleMantenimientoPredictivo.LimiteSup = MantenimientoPredictivod.LimiteSup;
                    db.DetalleMantenimientoPredictivo.Add(detalleMantenimientoPredictivo);
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
        [HttpPost, Route("CreateDetailActivity")]
        public async Task<HttpResponseMessage> PostMantenimientoPredictivoDetailActivity(ActividadMantenimientoPredictivoModel MantenimientoPredictivod)
        {
            try
            {
                ActividadMantenimientoPredictivo detalleMantenimientoPredictivo = new ActividadMantenimientoPredictivo();
                
                if (detalleMantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    detalleMantenimientoPredictivo.IdDetalleMantenimientoPredictivo = MantenimientoPredictivod.IdDetalleMantenimientoPredictivo;
                    detalleMantenimientoPredictivo.Descripcion = MantenimientoPredictivod.Descripcion;
                    detalleMantenimientoPredictivo.Tiempo = MantenimientoPredictivod.Tiempo;
                    detalleMantenimientoPredictivo.Ejecutada = MantenimientoPredictivod.Ejecutada;
                    db.ActividadMantenimientoPredictivo.Add(detalleMantenimientoPredictivo);
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [System.Web.Http.Route("CreateDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailMaterial(MantenimientoPredictivoMaterialModel guiam)
        {

            MantenimientoPredictivoMaterial mantenimientoPredictivo = new MantenimientoPredictivoMaterial();
            try
            {
                if (guiam != null)
                {
                    mantenimientoPredictivo.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    mantenimientoPredictivo.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    mantenimientoPredictivo.IdMaterial = guiam.IdMaterial;
                    mantenimientoPredictivo.Cantidad = guiam.Cantidad;
                    db.MantenimientoPredictivoMaterial.Add(mantenimientoPredictivo);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("CreateDetailRefection")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailRefection(MantenimientoPredictivoRefactionModel guiam)
        {

            MantenimientoPredictivoRefaccion mantenimientoPredictivo = new MantenimientoPredictivoRefaccion();
            try
            {
                if (guiam != null)
                {
                    mantenimientoPredictivo.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    mantenimientoPredictivo.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    mantenimientoPredictivo.IdRefaccion = guiam.IdReFaccion;
                    mantenimientoPredictivo.Cantidad = guiam.Cantidad;
                    db.MantenimientoPredictivoRefaccion.Add(mantenimientoPredictivo);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("CreateDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailJob(MantenimientoPredictivoJobModel guiam)
        {

            MantenimientoPredictivoPuesto backLogPuesto = new MantenimientoPredictivoPuesto();
            try
            {
                if (guiam != null)
                {
                    backLogPuesto.IdActividadMantenimientoPredictivo = guiam.IdActividadMantenimientoPredictivo;
                    backLogPuesto.IdDetalleMantenimientoPredictivo = guiam.IdDetalleMantenimientoPredictivo;
                    backLogPuesto.IdPuesto = guiam.IdPuesto;
                    backLogPuesto.Cantidad = guiam.Cantidad;
                    db.MantenimientoPredictivoPuesto.Add(backLogPuesto);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [HttpPost, Route("DeleteHeader")]
        public async Task<HttpResponseMessage> DeleteMantenimientoPredictivo(IdModelMantenimientoPredictivo Id)
        {
            try
            {
                _mantenimientoPredictivo = await db.MantenimientoPredictivo.FindAsync(Id.IdMantenimientoPredictivo);
                if (_mantenimientoPredictivo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    db.Entry(_mantenimientoPredictivo).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
        [HttpPost, Route("DeleteDetail")]
        public async Task<HttpResponseMessage> DeleteMantenimientoPredictivoDetail(IdModelMantenimientoPredictivoDetails Id)
        {
            DetalleMantenimientoPredictivo guia = new DetalleMantenimientoPredictivo();
            try
            {
                if (Id != null)
                {
                    guia = db.DetalleMantenimientoPredictivo.Where(x => x.IdDetalleMantenimientoPredictivo == Id.IdMantenimientoPredictivodetail).FirstOrDefault();
                    if (guia != null)
                        db.DetalleMantenimientoPredictivo.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("DeleteDetailActivity")]
        public async Task<HttpResponseMessage> DeleteMantenimientoPredictivoDetailActivity(IdModelMantenimientoPredictivoDetails Id)
        {
            ActividadMantenimientoPredictivo guia = new ActividadMantenimientoPredictivo();
            try
            {
                if (Id != null)
                {
                    guia = db.ActividadMantenimientoPredictivo.Where(x => x.IdDetalleMantenimientoPredictivo == Id.IdMantenimientoPredictivoActivity).FirstOrDefault();
                    if (guia != null)
                        db.ActividadMantenimientoPredictivo.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [System.Web.Http.Route("DeleteDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailMaterial(IdModelMantenimientoPredictivoDetails id)
        {

            MantenimientoPredictivoMaterial guia = new MantenimientoPredictivoMaterial();
            try
            {
                if (id != null)
                {
                    guia = db.MantenimientoPredictivoMaterial.Where(x => x.IdMantenimientoPredictivoMaterial == id.IdMantenimientoPredictivoMaterial).FirstOrDefault();
                    if (guia != null)
                        db.MantenimientoPredictivoMaterial.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [System.Web.Http.Route("DeleteDetailRefaction")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailRefaction(IdModelMantenimientoPredictivoDetails id)
        {

            MantenimientoPredictivoRefaccion guia = new MantenimientoPredictivoRefaccion();
            try
            {
                if (id != null)
                {
                    guia = db.MantenimientoPredictivoRefaccion.Where(x => x.IdMantenimientoPredictivoRefaccion == id.IdMantenimientoPredictivoRefaction).FirstOrDefault();
                    if (guia != null)
                        db.MantenimientoPredictivoRefaccion.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("DeleteDetailJob")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailJob(IdModelMantenimientoPredictivoDetails id)
        {

            MantenimientoPredictivoPuesto guia = new MantenimientoPredictivoPuesto();
            try
            {
                if (id != null)
                {
                    guia = db.MantenimientoPredictivoPuesto.Where(x => x.IdMantenimientoPredictivoPuesto == id.IdMantenimientoPredictivoJob).FirstOrDefault();
                    if (guia != null)
                        db.MantenimientoPredictivoPuesto.Remove(guia);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mantenimientosPredictivo = null;
                _mantenimientoPredictivo = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelMantenimientoPredictivo
        {
            [DataMember]
            public int IdMantenimientoPredictivo { get; set; }
        }
        [DataContract]
        public class IdModelMantenimientoPredictivoDetails
        {
            [DataMember]
            public int IdMantenimientoPredictivoActivity { get; set; }
            [DataMember]
            public int IdMantenimientoPredictivodetail { get; set; }
            [DataMember]
            public int IdMantenimientoPredictivoMaterial { get; set; }
            [DataMember]
            public int IdMantenimientoPredictivoJob { get; set; }
            [DataMember]
            public int IdMantenimientoPredictivoRefaction { get; set; }
        }
    }
}