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
    [RoutePrefix("FastMan/Unidades")]
    public class UnidadesController : ApiController
    {
        private DBConnection db = new DBConnection();
        private List<Unidad> _unidades;
        private Unidad _unidad;

        [HttpPost, Route("Index")]
        public async Task<HttpResponseMessage> GetUnidad()
        {
            try
            {
                _unidades = await db.Unidad.Where(x => x.Activo_Inactivo ==  true).ToListAsync().ConfigureAwait(false);
                List<UnidadModel> unidads = new List<UnidadModel>();
                foreach (var unidad in _unidades)
                {
                    var _unidad = new UnidadModel();
                    _unidad.IdUnidad = unidad.IdUnidad;
                    _unidad.IdCategoriaUnidad = unidad.IdCategoriaUnidad;                   
                    _unidad.IdMarca = unidad.IdMarca;
                    _unidad.IdModelo = unidad.IdModelo;
                    _unidad.IdTrabajador = unidad.Encargado;
                    _unidad.Codigo = unidad.Codigo;
                    _unidad.Año = unidad.Año;
                    _unidad.Color = unidad.Color;
                    _unidad.Dimensiones = unidad.Dimensiones;
                    _unidad.Matricula = unidad.Matricula;
                    _unidad.NumeroChasis = unidad.NumeroChasis;
                    _unidad.PeriodoServicio = unidad.PeriodoServicio;
                    _unidad.PrecioCompra = unidad.PrecioCompra;
                    _unidad.MontoModificacion = unidad.MontoModificacion;
                    _unidad.MontoRecuperacion = unidad.MontoRecuperacion;
                    _unidad.Km_Hr = unidad.Km_Hr;
                    _unidad.Activo_Inactivo = unidad.Activo_Inactivo;
                    unidads.Add(_unidad);

                }

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(unidads), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

        }

        [HttpPost, Route("Detail")]
        public async Task<HttpResponseMessage> GetUnidad(IdModelUnidad Id)
        {
            try
            {
                _unidad = await db.Unidad.FindAsync(Id.IdUnidad);
                if (_unidad == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    var unidad = new UnidadModel();

                    unidad.IdUnidad = _unidad.IdUnidad;
                    unidad.IdCategoriaUnidad = _unidad.IdCategoriaUnidad;
                    unidad.IdMarca = _unidad.IdMarca;
                    unidad.IdModelo = _unidad.IdModelo;
                    unidad.IdTrabajador = _unidad.Encargado;
                    unidad.Codigo = _unidad.Codigo;
                    unidad.Año = _unidad.Año;
                    unidad.Color = _unidad.Color;
                    unidad.Dimensiones = _unidad.Dimensiones;
                    unidad.Matricula = _unidad.Matricula;
                    unidad.ImagenVehiculo = _unidad.ImagenVehiculo;
                    unidad.NumeroChasis = _unidad.NumeroChasis;
                    unidad.PeriodoServicio = _unidad.PeriodoServicio;
                    unidad.PrecioCompra = _unidad.PrecioCompra;
                    unidad.MontoModificacion = _unidad.MontoModificacion;
                    unidad.MontoRecuperacion = _unidad.MontoRecuperacion;
                    unidad.Km_Hr = _unidad.Km_Hr;
                    unidad.Activo_Inactivo = _unidad.Activo_Inactivo;

                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(unidad), System.Text.Encoding.UTF8, "application/json") };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<HttpResponseMessage> PutUnidad(UnidadModel Unidad)
        {
            try
            {
                _unidad = await db.Unidad.FindAsync(Unidad.IdUnidad);
                if (_unidad == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _unidad.IdCategoriaUnidad = Unidad.IdCategoriaUnidad;
                    _unidad.IdMarca = Unidad.IdMarca;
                    _unidad.IdModelo = Unidad.IdModelo;
                    _unidad.Encargado = Unidad.IdTrabajador;
                    _unidad.Codigo = Unidad.Codigo;
                    _unidad.Año = Unidad.Año;
                    _unidad.Color = Unidad.Color;
                    _unidad.Dimensiones = Unidad.Dimensiones;
                    _unidad.Matricula = Unidad.Matricula;
                    _unidad.ImagenVehiculo = Unidad.ImagenVehiculo;
                    _unidad.NumeroChasis = Unidad.NumeroChasis;
                    _unidad.PeriodoServicio = Unidad.PeriodoServicio;
                    _unidad.PrecioCompra = Unidad.PrecioCompra;
                    _unidad.MontoModificacion = Unidad.MontoModificacion;
                    _unidad.MontoRecuperacion = Unidad.MontoRecuperacion;
                    _unidad.Km_Hr = Unidad.Km_Hr;

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
        public async Task<HttpResponseMessage> PostUnidad(UnidadModel Unidad)
        {
            try
            {
                var _unidad = new Unidad();

                _unidad.IdCategoriaUnidad = Unidad.IdCategoriaUnidad;
                _unidad.IdMarca = Unidad.IdMarca;
                _unidad.IdModelo = Unidad.IdModelo;
                _unidad.Encargado = Unidad.IdTrabajador;
                _unidad.Codigo = Unidad.Codigo;
                _unidad.Año = Unidad.Año;
                _unidad.Color = Unidad.Color;
                _unidad.Dimensiones = Unidad.Dimensiones;
                _unidad.Matricula = Unidad.Matricula;
                _unidad.ImagenVehiculo = Unidad.ImagenVehiculo;
                _unidad.NumeroChasis = Unidad.NumeroChasis;
                _unidad.PeriodoServicio = Unidad.PeriodoServicio;
                _unidad.PrecioCompra = Unidad.PrecioCompra;
                _unidad.MontoModificacion = Unidad.MontoModificacion;
                _unidad.MontoRecuperacion = Unidad.MontoRecuperacion;
                _unidad.Km_Hr = Unidad.Km_Hr;
                _unidad.Activo_Inactivo = true;

                db.Unidad.Add(_unidad);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<HttpResponseMessage> DeleteUnidad(IdModelUnidad Id)
        {
            try
            {
                _unidad = await db.Unidad.FindAsync(Id.IdUnidad);
                if (_unidad == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    _unidad.Activo_Inactivo = false;
                    db.Entry(_unidad).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }


        [HttpPost, Route("AssingUnidadArea")]
        public async Task<HttpResponseMessage> AssingUnidadArea(AssingUnidadAreaModel assing)
        {
            try
            {
                UnidadArea acu = new UnidadArea();

                acu.IdArea = assing.IdArea;
                acu.IdUnidad = assing.IdUnidad;
                db.UnidadArea.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingUnidadProceso")]
        public async Task<HttpResponseMessage> AssingUnidadProceso(AssingUnidadProcesoModel assing)
        {
            try
            {
                UnidadProceso acu = new UnidadProceso();

                acu.IdProceso = assing.IdProceso;
                acu.IdUnidad = assing.IdUnidad;
                db.UnidadProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingUnidadSubProceso")]
        public async Task<HttpResponseMessage> AssingUnidadSubProceso(AssingUnidadSubProcesoModel assing)
        {
            try
            {
                UnidadSubProceso acu = new UnidadSubProceso();

                acu.IdSubProceso = assing.IdSubProceso;
                acu.IdUnidad = assing.IdUnidad;
                db.UnidadSubProceso.Add(acu);
                await db.SaveChangesAsync();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }

        [HttpPost, Route("AssingUnidadUbicacion")]
        public async Task<HttpResponseMessage> AssingUnidadUbicacion(AssingUnidadUbicacionModel assing)
        {
            try
            {
                UnidadUbicacion acu = new UnidadUbicacion();

                acu.IdUbicacion = assing.IdUbicacion;
                acu.IdUnidad = assing.IdUnidad;
                db.UnidadUbicacion.Add(acu);
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
                _unidades = null;
                _unidad = null;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [DataContract]
        public class IdModelUnidad
        {
            [DataMember]
            public int IdUnidad { get; set; }
        }

        [DataContract]
        public class UnidadModel
        {
            [DataMember]
            public int IdUnidad { get; set; }
            [DataMember]
            public int IdCategoriaUnidad { get; set; }
            [DataMember]
            public int IdUbicacion { get; set; }
            [DataMember]
            public int IdMarca { get; set; }
            [DataMember]
            public int IdModelo { get; set; }
            [DataMember]
            public int IdTrabajador { get; set; }
            [DataMember]
            public string Codigo { get; set; }
            [DataMember]
            public Nullable<int> Año { get; set; }
            [DataMember]
            public string Color { get; set; }
            [DataMember]
            public string Dimensiones { get; set; }
            [DataMember]
            public string Matricula { get; set; }
            [DataMember]
            public byte[] ImagenVehiculo { get; set; }
            [DataMember]
            public string NumeroChasis { get; set; }
            [DataMember]
            public int PeriodoServicio { get; set; }
            [DataMember]
            public double PrecioCompra { get; set; }
            [DataMember]
            public double MontoModificacion { get; set; }
            [DataMember]
            public double MontoRecuperacion { get; set; }
            [DataMember]
            public Nullable<bool> Km_Hr { get; set; }
            [DataMember]
            public Nullable<bool> Activo_Inactivo { get; set; }            
        }

        public class AssingUnidadAreaModel
        {
            public int IdUnidadArea { get; set; }
            public int IdArea { get; set; }
            public int IdUnidad { get; set; }
        }
        public class AssingUnidadProcesoModel
        {
            public int IdProceso { get; set; }
            public int IdUnidad { get; set; }
        }
        public class AssingUnidadSubProcesoModel
        {
            public int IdSubProceso { get; set; }
            public int IdUnidad { get; set; }
        }
        public class AssingUnidadUbicacionModel
        {
            public int IdUbicacion { get; set; }
            public int IdUnidad { get; set; }
        }
    }
}