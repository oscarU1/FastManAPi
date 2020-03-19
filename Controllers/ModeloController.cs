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
    [System.Web.Http.RoutePrefix("FastMan/Modelo")]
    public class ModeloController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Modelo> acu = new List<Modelo>();
            try
            {
                acu = db.Modelo.ToList();
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
        public HttpResponseMessage Create(ModeloModel area)
        {
            Modelo acu = new Modelo();
            try
            {
                if (area != null)
                {
                    acu.IdMarca = area.IdMarca;
                    acu.Descripcion = area.Descripcion;
                    db.Modelo.Add(acu);
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
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdModeloModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Modelo tipo = new Modelo();
                    tipo = await db.Modelo.FindAsync(id.IdModelo);
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
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(ModeloModel evento)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Modelo tipo = new Modelo();
                    tipo = await db.Modelo.Where(x => x.IdModelo == evento.IdModelo).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        tipo.IdMarca = evento.IdMarca;
                        tipo.Descripcion = evento.Descripcion;
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
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(IdModeloModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Modelo tipo = new Modelo();
                    tipo = await db.Modelo.FindAsync(id.IdModelo);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        db.Modelo.Remove(tipo);
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
        public class ModeloModel
        {
            [DataMember]
            public int IdMarca { get; set; }
            [DataMember]
            public int IdModelo { get; set; }
            [DataMember]
            public string Descripcion { get; set; }
        }

        [DataContract]
        public class IdModeloModel
        {
            [DataMember]
            public int IdModelo { get; set; }
        }
    }

}
