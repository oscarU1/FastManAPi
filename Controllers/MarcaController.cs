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

namespace apifastman.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("FastMan/Marca")]
    public class MarcaController : ApiController
    {
        private DBConnection db = new DBConnection();

        [System.Web.Http.Route("Index")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Index()
        {
            List<Marca> acu = new List<Marca>();
            try
            {
                acu = db.Marca.AsNoTracking().ToList();
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
        public HttpResponseMessage Create(MarcaModel area)
        {
            Marca acu = new Marca();
            try
            {
                if (area != null)
                {
                    acu.IdArea = area.IdArea;
                    acu.Descripcion = area.Descripcion;                   
                    db.Marca.Add(acu);
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
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + "Marca");
            }

        }

        [System.Web.Http.Route("Details")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> GetEventosGuiaInspeccion(IdMarcaModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Marca tipo = new Marca();
                    tipo = await db.Marca.FindAsync(id.IdMarca);
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
        public async Task<HttpResponseMessage> PutEventosGuiaInspeccion(MarcaModel evento)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Marca tipo = new Marca();
                    tipo = await db.Marca.Where(x => x.IdMarca == evento.IdMarca).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        tipo.IdArea = evento.IdArea;
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
        public async Task<HttpResponseMessage> DeleteEventosGuiaInspeccion(IdMarcaModel id)
        {
            using (db = new DBConnection())
            {
                try
                {
                    Marca tipo = new Marca();
                    tipo = await db.Marca.FindAsync(id.IdMarca);
                    if (tipo == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        db.Marca.Remove(tipo);
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
        public class MarcaModel
        {
            [DataMember]
            public int IdMarca { get; set; }
            [DataMember]
            public int IdArea { get; set; }
            [DataMember]
            public string Descripcion { get; set; }
        }
        [DataContract]
        public class IdMarcaModel
        {
            [DataMember]
            public int IdMarca { get; set; }           
        }

    }

}
