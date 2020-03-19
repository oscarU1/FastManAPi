using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using core.Models;
using System.Runtime.Serialization;

namespace core.Controllers
{
    [RoutePrefix("FastMan/SistemaRefacciones")]
    public class SistemaRefaccionesController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<SistemaRefacciones> acu;
            try
            {
                acu = db.SistemaRefacciones.Where(x=>x.Activo_Inactivo== true).ToList();
                if (acu != null)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(acu), System.Text.Encoding.UTF8, "application/json") };
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Marca");
            }

        }

        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(SistemaRefaccionesModel area)
        {
            SistemaRefacciones acu = new SistemaRefacciones();
            try
            {
                if (area != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.NombreSistema = area.NombreSistema;
                    db.SistemaRefacciones.Add(acu);
                    db.SaveChanges();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = null };
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { Content = null };
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Area");
            }

        }

        [System.Web.Http.Route("Details")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdSistemaRefaccionesModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    SistemaRefacciones tipo = new SistemaRefacciones();
                    tipo = await db.SistemaRefacciones.FindAsync(id.IdSistemaRefacciones);
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

        [System.Web.Http.Route("Edit")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(SistemaRefaccionesModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    SistemaRefacciones tipo = new SistemaRefacciones();
                    tipo = await db.SistemaRefacciones.Where(x => x.IdSistemaRefacciones == id.IdSistemaRefacciones).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        tipo.IdArea = id.IdArea;
                        tipo.NombreSistema = id.NombreSistema;
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

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(IdSistemaRefaccionesModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    SistemaRefacciones tipo = new SistemaRefacciones();
                    tipo = await db.SistemaRefacciones.FindAsync(id.IdSistemaRefacciones);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        db.SistemaRefacciones.Remove(tipo);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class SistemaRefaccionesModel
        {
            [DataMember]
            public int IdSistemaRefacciones { get; set; }
            [DataMember]
            public int IdArea { get; set; }
            [DataMember]
            public int IdModelo { get; set; }
            [DataMember]
            public string NombreSistema { get; set; }
        }

        [DataContract]
        public class IdSistemaRefaccionesModel
        {
            [DataMember]
            public int IdSistemaRefacciones { get; set; }
        }
    }
}
