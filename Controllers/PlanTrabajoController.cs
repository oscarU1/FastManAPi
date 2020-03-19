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
using System.Runtime.Serialization;

namespace apifastman.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("FastMan/PlanTrabajo")]
    public class PlanTrabajoController : ApiController
    {
        private DBConnection db = new DBConnection();

        //[System.Web.Http.Route("Index")]
        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage Index()
        //{
        //    List<PlanTrabajo> acu = new List<PlanTrabajo>();
        //    try
        //    {
        //        acu = db.PlanTrabajo.ToList();
        //        if (acu != null)
        //            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu)) };
        //        else
        //            return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
        //    }

        //}

        [System.Web.Http.Route("Detail")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Detail(IdModelPlanTrabajo Id)
        {
            List<PlanTrabajo> trabajo = new List<PlanTrabajo>();
            List<DetalleOrdenTrabajo> guia = new List<DetalleOrdenTrabajo>();
            DetalleOrdenTrabajo guia1 = new DetalleOrdenTrabajo();
            try
            {
                if (Id != null)
                {
                    trabajo = db.PlanTrabajo.Where(x => x.IdOrdenTrabajo == Id.IdOrdenTrabajo).ToList();
                    foreach (PlanTrabajo details in trabajo)
                    {
                        guia1 = db.DetalleOrdenTrabajo.Where(x => x.IdOrdenTrabajo == details.IdOrdenTrabajo).FirstOrDefault();
                        guia.Add(guia1);
                    }
                }
                if (guia != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(guia), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
            }

        }


        [System.Web.Http.Route("GenerateWorkPlan")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GenerateWorkPlan(IdModelPlanTrabajo guiam)
        {
            try
            {
                OrdenTrabajo ordenTrabajo = new OrdenTrabajo();
                List<DetalleOrdenTrabajo> detalleOrdenTrabajos = new List<DetalleOrdenTrabajo>();
                if (guiam.IdOrdenTrabajo != 0)
                {
                    ordenTrabajo = db.OrdenTrabajo.Where(x => x.IdOrdenTrabajo == guiam.IdOrdenTrabajo).FirstOrDefault();
                    if (ordenTrabajo != null)
                    {
                        detalleOrdenTrabajos = db.DetalleOrdenTrabajo.Where(x => x.IdOrdenTrabajo == ordenTrabajo.IdOrdenTrabajo).ToList();
                    }
                    else
                    {
                        throw new Exception("No existe Orden Trabajo");
                    }
                }
                else
                    throw new Exception("Datos Requeridos no enviados");

                PlanTrabajo planTrabajo = new PlanTrabajo();                            
                foreach(DetalleOrdenTrabajo detalle in detalleOrdenTrabajos)
                {
                    planTrabajo.IdOrdenTrabajo = detalle.IdOrdenTrabajo;
                    planTrabajo.IdDetalleOrdenTrabajo = detalle.IdDetalleOrdenTrabajo;
                    planTrabajo.IdArea = ordenTrabajo.IdArea;
                    planTrabajo.TipoServicio = detalle.TipoServicio;
                    planTrabajo.Actividad = detalle.Actividad;
                    planTrabajo.Tiempo = detalle.Tiempo;
                    planTrabajo.FechaPlanInicio = DateTime.Now;
                    planTrabajo.FechaPlanFin = DateTime.Now;
                    planTrabajo.Comienza = TimeSpan.FromHours(0);
                    db.PlanTrabajo.Add(planTrabajo);
                    db.SaveChangesAsync();
                    ordenTrabajo.Planeada = true;
                    db.SaveChangesAsync();
                }

                return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
            }

        }

        [System.Web.Http.Route("AssingDatesWorkPlan")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AssingDatesWorkPlan(AssingDatesWorkPlanModel guiam)
        {

            PlanTrabajo planTrabajo = new PlanTrabajo();
            try
            {
                if (guiam.IdPlanTrabajo != 0)
                {
                    planTrabajo = db.PlanTrabajo.Where(x => x.IdPlanTrabajo == guiam.IdPlanTrabajo).FirstOrDefault();
                    planTrabajo.FechaPlanFin = guiam.FechaFin;
                    planTrabajo.FechaPlanInicio = guiam.Fechainicio;
                    planTrabajo.Comienza = guiam.Comienza;
                    db.SaveChanges();

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
            }

        }


        [System.Web.Http.Route("AsignWorker")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AsignWorker(DetallePlanTrabajoModel guiam)
        {

            DetallePlanTrabajoTrabajador detallePlan = new DetallePlanTrabajoTrabajador();
            try
            {
                if (guiam != null)
                {

                    detallePlan.IdPlanTrabajo = guiam.IdPlanTrabajo;
                    detallePlan.IdTrabajador = guiam.IdTrabajador;
                    db.DetallePlanTrabajoTrabajador.Add(detallePlan);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
            }

        }

        [System.Web.Http.Route("EditWorker")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EditWorker(DetallePlanTrabajoModel guiam)
        {

            DetallePlanTrabajoTrabajador planTrabajo = new DetallePlanTrabajoTrabajador();
            try
            {
                if (guiam != null)
                {
                    planTrabajo = db.DetallePlanTrabajoTrabajador.Where(x => x.IdDetallePlanTrabajo == guiam.IdDetallePlanTrabajo).FirstOrDefault();
                    planTrabajo.IdTrabajador = guiam.IdTrabajador;
                    db.SaveChanges();

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "PlanTrabajo");
            }

        }
        [System.Web.Http.Route("DeleteHeader")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteHeader(IdModelPlanTrabajo id)
        {

            PlanTrabajo planTrabajo = new PlanTrabajo();
            DetallePlanTrabajoTrabajador detaplanTrabajo = new DetallePlanTrabajoTrabajador();
            try
            {
                if (id.IdPlanTrabajo != 0)
                {
                    planTrabajo = db.PlanTrabajo.Where(x => x.IdPlanTrabajo == id.IdPlanTrabajo).FirstOrDefault();
                    if (planTrabajo != null)
                    {
                        detaplanTrabajo = db.DetallePlanTrabajoTrabajador.Where(x => x.IdPlanTrabajo == planTrabajo.IdPlanTrabajo).FirstOrDefault();
                        db.DetallePlanTrabajoTrabajador.Remove(detaplanTrabajo);
                        db.PlanTrabajo.Remove(planTrabajo);
                        db.SaveChanges();
                        return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                    }
                    else
                        throw new Exception("Error Al eliminar Plan de Trabjo no existe");
                }
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "BackLog");
            }

        }

        [System.Web.Http.Route("DeleteWorker")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteDetailMaterial(IdModelPlanTrabajo id)
        {

            DetallePlanTrabajoTrabajador detallePlan = new DetallePlanTrabajoTrabajador();
            try
            {
                if (id != null)
                {
                    detallePlan = db.DetallePlanTrabajoTrabajador.Where(x => x.IdPlanTrabajo == id.IdPlanTrabajo).FirstOrDefault();
                    if (detallePlan != null)
                        db.DetallePlanTrabajoTrabajador.Remove(detallePlan);
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
        public class IdModelPlanTrabajo
        {
            /// <summary>
            /// 
            /// </summary>
            [DataMember]
            public int IdPlanTrabajo { get; set; }
            /// <summary>
            /// este se llamara cuando sea a details del plan de trabajo
            /// </summary>
            [DataMember]
            public int IdOrdenTrabajo { get; set; }
        }

        [DataContract]
        public class AssingDatesWorkPlanModel
        {
            [DataMember]
            public int IdPlanTrabajo { get; set; }
            [DataMember]
            public DateTime Fechainicio { get; set; }
            [DataMember]
            public DateTime FechaFin { get; set; }

            [DataMember]
            public TimeSpan Comienza { get; set; }
        }
    }

}
