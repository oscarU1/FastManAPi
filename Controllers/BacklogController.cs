using System; using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using core.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace apifastman.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("FastMan/Backlog")]
    public class BacklogController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<BackLog> acu = new List<BackLog>();
            try
            {
                acu = db.BackLog.Where(x=>x.Unidad.Activo_Inactivo==true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> DetailAsync(IdModelBacklog Id)
        {
            BackLog guia = new BackLog();
            try
            {
                if (Id != null)
                {
                    guia = await db.BackLog.FindAsync(Id.IdBackLog).ConfigureAwait(false);
                }
                if (guia != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(guia), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }
        [System.Web.Http.Route("CreateHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateHeader(BackLogModel guiam)
        {

            BackLog backLog = new BackLog();
            try
            {
                int idback = 0;
                if (guiam != null)
                {
                    
                    backLog.IdUnidad = guiam.IdUnidad;
                    backLog.Fecha = guiam.Fecha;
                    backLog.Actividad = guiam.Actividad;
                    backLog.Tiempo = guiam.Tiempo;
                    backLog.Ejecutada = false;
                    db.BackLog.Add(backLog);
                    db.SaveChanges();

                    DateTime fecha = Convert.ToDateTime(guiam.Fecha.ToShortDateString());
                    var guia = db.BackLog.Where(x => x.IdUnidad == x.IdUnidad && x.Actividad == guiam.Actividad && x.Fecha == fecha && x.Tiempo == guiam.Tiempo).FirstOrDefault();
                    idback = guia.IdBackLog;
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(idback), System.Text.Encoding.UTF8, "application/json") };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [System.Web.Http.Route("CreateDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateDetailMaterial(BackLogMaterialModel guiam)
        {

            BackLogMaterial backLogMaterial = new BackLogMaterial();
            try
            {
                if (guiam != null)
                {
                    backLogMaterial.IdBackLog = guiam.IdBackLog;
                    backLogMaterial.IdMaterial = guiam.IdMaterial;
                    backLogMaterial.Cantidad = guiam.Cantidad;
                    db.BackLogMaterial.Add(backLogMaterial);
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
        public HttpResponseMessage CreateDetailRefection(BackLogRefactionModel guiam)
        {

            BackLogRefaccion backLogRefaccion = new BackLogRefaccion();
            try
            {
                if (guiam != null)
                {
                    backLogRefaccion.IdBackLog = guiam.IdBackLog;
                    backLogRefaccion.IdReFaccion = guiam.IdReFaccion;
                    backLogRefaccion.Cantidad = guiam.Cantidad;
                    db.BackLogRefaccion.Add(backLogRefaccion);
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
        public HttpResponseMessage CreateDetailJob(BackLogJobModel guiam)
        {

            BackLogPuesto backLogPuesto = new BackLogPuesto();
            try
            {
                if (guiam != null)
                {
                    backLogPuesto.IdBackLog = guiam.IdBackLog;
                    backLogPuesto.IdPuesto = guiam.IdPuesto;
                    backLogPuesto.Cantidad = guiam.Cantidad;
                    db.BackLogPuesto.Add(backLogPuesto);
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
        [System.Web.Http.Route("EditHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditHeader(BackLogModel guiam)
        {

            BackLog backLog = new BackLog();
            try
            {
                if (guiam != null)
                {
                    backLog = db.BackLog.Where(x => x.IdBackLog == guiam.IdBackLog).FirstOrDefault();
                    
                    backLog.IdUnidad = guiam.IdUnidad;
                    backLog.Fecha = guiam.Fecha;
                    backLog.Actividad = guiam.Actividad;
                    backLog.Tiempo = guiam.Tiempo;
                    backLog.Ejecutada = guiam.Ejecutada;
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

        [System.Web.Http.Route("EditDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditDetailMaterial(BackLogMaterialModel guiam)
        {
            BackLogMaterial backLogMaterial = new BackLogMaterial();
            try
            {
                if (guiam != null)
                {
                    backLogMaterial = db.BackLogMaterial.Where(x => x.IdBackLogMaterial == guiam.IdBackLogMaterial).FirstOrDefault();
                    backLogMaterial.IdBackLog = guiam.IdBackLog;
                    backLogMaterial.IdMaterial = guiam.IdMaterial;
                    backLogMaterial.Cantidad = guiam.Cantidad;
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
        public HttpResponseMessage EditDetailRefection(BackLogRefactionModel guiam)
        {

            BackLogRefaccion backLogRefaccion = new BackLogRefaccion();
            try
            {
                if (guiam != null)
                {
                    backLogRefaccion = db.BackLogRefaccion.Where(x => x.IdBackLogRefaccion == guiam.IdBackLogRefaccion).FirstOrDefault();
                    backLogRefaccion.IdBackLog = guiam.IdBackLog;
                    backLogRefaccion.IdReFaccion = guiam.IdReFaccion;
                    backLogRefaccion.Cantidad = guiam.Cantidad;
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
        public HttpResponseMessage EditDetailJob(BackLogJobModel guiam)
        {

            BackLogPuesto backLogPuesto = new BackLogPuesto();
            try
            {
                if (guiam != null)
                {
                    backLogPuesto = db.BackLogPuesto.Where(x => x.IdBackLogPuesto == guiam.IdBackLogPuesto).FirstOrDefault();
                    backLogPuesto.IdBackLog = guiam.IdBackLog;
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
        [System.Web.Http.Route("ExecutedHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage ExecutedHeader(IdModelBacklog id)
        {

            BackLog guia = new BackLog();
            try
            {
                if (id != null)
                {
                    guia = db.BackLog.Where(x => x.IdBackLog == id.IdBackLog).FirstOrDefault();
                    guia.Ejecutada = true;
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
        [System.Web.Http.Route("DeleteHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteHeader(IdModelBacklog id)
        {

            BackLog guia = new BackLog();
            List<BackLogMaterial> backLogMaterial = new List<BackLogMaterial>();
            List<BackLogPuesto> backLogPuesto = new List<BackLogPuesto>();
            List<BackLogRefaccion> backLogRefaccion = new List<BackLogRefaccion>();
            try
            {
                if (id != null)
                {
                    backLogMaterial = db.BackLogMaterial.Where(x => x.IdBackLog == id.IdBackLog).ToList();
                    if (backLogMaterial != null)
                    {
                        foreach (BackLogMaterial logMaterial in backLogMaterial)
                        {
                            db.BackLogMaterial.Remove(logMaterial);
                        }
                    }
                    backLogPuesto = db.BackLogPuesto.Where(x => x.IdBackLog == id.IdBackLog).ToList();
                    if (backLogPuesto != null)
                    {
                        foreach (BackLogPuesto logMaterial in backLogPuesto)
                        {
                            db.BackLogPuesto.Remove(logMaterial);
                        }
                    }
                    backLogRefaccion = db.BackLogRefaccion.Where(x => x.IdBackLog == id.IdBackLog).ToList();
                    if (backLogRefaccion != null)
                    {
                        foreach (BackLogRefaccion logMaterial in backLogRefaccion)
                        {
                            db.BackLogRefaccion.Remove(logMaterial);
                        }
                    }
                    guia = db.BackLog.Where(x => x.IdBackLog == id.IdBackLog).FirstOrDefault();
                    db.BackLog.Remove(guia);
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

        [System.Web.Http.Route("DeleteDetailMaterial")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailMaterial(IdModelBacklogDetails id)
        {

            BackLogMaterial guia = new BackLogMaterial();
            try
            {
                if (id != null)
                {
                    guia = db.BackLogMaterial.Where(x => x.IdBackLogMaterial == id.IdBackLogMaterial).FirstOrDefault();
                    if (guia != null)
                        db.BackLogMaterial.Remove(guia);
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
        public HttpResponseMessage DeleteDetailRefaction(IdModelBacklogDetails id)
        {

            BackLogRefaccion guia = new BackLogRefaccion();
            try
            {
                if (id != null)
                {
                    guia = db.BackLogRefaccion.Where(x => x.IdBackLogRefaccion == id.IdBackLogRefaction).FirstOrDefault();
                    if (guia != null)
                        db.BackLogRefaccion.Remove(guia);
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
        public HttpResponseMessage DeleteDetailJob(IdModelBacklogDetails id)
        {

            BackLogPuesto guia = new BackLogPuesto();
            try
            {
                if (id != null)
                {
                    guia = db.BackLogPuesto.Where(x => x.IdBackLogPuesto == id.IdBackLogJob).FirstOrDefault();
                    if (guia != null)
                        db.BackLogPuesto.Remove(guia);
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

                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelBacklog
        {
            [DataMember]
            public int IdBackLog { get; set; }
        }
        [DataContract]
        public class IdModelBacklogDetails
        {
            [DataMember]
            public int IdBackLogMaterial { get; set; }
            [DataMember]
            public int IdBackLogJob { get; set; }
            [DataMember]
            public int IdBackLogRefaction { get; set; }
        }
    }    
}
