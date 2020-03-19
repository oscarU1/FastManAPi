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

namespace apifastman.Controllers
{
    [RoutePrefix("FastMan/Usuarios")]
    public class UsuariosController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Usuario> _usuarios;
        private Usuario _usuario;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetUsuario()
        {
            try
            {   _usuarios = await db.Usuario.Where(x => x.Activo_Inactivo == true).ToListAsync().ConfigureAwait(false);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_usuarios), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetUsuario(IdModelUsuario Id)
        {
            try
            {
                _usuario = await db.Usuario.FindAsync(Id.IdUsuario);
                if (_usuario == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_usuario), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutUsuario(Usuario usuario)
        {
            try
            {
                _usuario = await db.Usuario.FindAsync(usuario.IdUsuario);
                if (_usuario == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {

                    _usuario.Contraseña_Usuario = usuario.Contraseña_Usuario;
                    _usuario.CorreoElectronicoUsuario = usuario.CorreoElectronicoUsuario;
                    _usuario.NombreUsuario = usuario.NombreUsuario;
                    _usuario.TelefonoUsuario = usuario.TelefonoUsuario;

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
        public async Task<HttpResponseMessage> PostUsuario(Usuario usuario)
        {
            try
            {
                usuario.Activo_Inactivo = true;
                db.Usuario.Add(usuario);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteUsuario(IdModelUsuario Id)
        {
            try
            {
                _usuario = await db.Usuario.FindAsync(Id.IdUsuario);
                if (_usuario == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _usuario.Activo_Inactivo = false;
                    db.Entry(_usuario).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Login")]
        public async Task<HttpResponseMessage> Login(LoginData loginData)
        {
            try
            {
                if (db.Usuario.AnyAsync(e => e.CorreoElectronicoUsuario.Equals(loginData.userName, StringComparison.InvariantCultureIgnoreCase)).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    _usuarios = await db.Usuario.ToListAsync().ConfigureAwait(false);
                    _usuario = _usuarios.Find(e => e.CorreoElectronicoUsuario.Equals(loginData.userName, StringComparison.InvariantCultureIgnoreCase));
                    if (_usuario.Contraseña_Usuario.Equals(loginData.userPassword))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_usuario, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, ReferenceLoopHandling = ReferenceLoopHandling.Serialize }), System.Text.Encoding.UTF8, "application/json") };
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Contraseña incorrecta.") };
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Usuario Incorrecto") };
                }
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("AssingUserToArea")]
        public async Task<HttpResponseMessage> AssingUserToArea(AssingUserToAreaModel assing)
        {
            try
            {
                UsuarioArea acu = new UsuarioArea();

                acu.IdArea = assing.IdArea;
                acu.IdUsuario = assing.IdUsuario;
                db.UsuarioArea.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingUserToProcess")]
        public async Task<HttpResponseMessage> AssingUserToProcess(AssingUserToProcessModel assing)
        {
            try
            {
                UsuarioProceso acu = new UsuarioProceso();

                acu.IdProceso = assing.IdProceso;
                acu.IdUsuario = assing.IdUsuario;
                db.UsuarioProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingUserToSubProcess")]
        public async Task<HttpResponseMessage> AssingUserToSubProcess(AssingUserToSubProcessModel assing)
        {
            try
            {
                UsuarioSubProceso acu = new UsuarioSubProceso();

                acu.IdSubProceso = assing.IdSubProceso;
                acu.IdUsuario = assing.IdUsuario;
                db.UsuarioSubProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
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
                _usuarios = null;
                _usuario = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelUsuario
        {
            [DataMember]
            public int IdUsuario { get; set; }
        }
    }

    public class LoginData
    {
        public string userName { get; set; }
        public string userPassword { get; set; }
    }
    public class AssingUserToAreaModel
    {
        public int IdArea { get; set; }
        public int IdUsuario { get; set; }
    }
    public class AssingUserToProcessModel
    {
        public int IdProceso { get; set; }
        public int IdUsuario { get; set; }
    }
    public class AssingUserToSubProcessModel
    {
        public int IdSubProceso { get; set; }
        public int IdUsuario { get; set; }
    }
}