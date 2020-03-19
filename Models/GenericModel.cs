using System; using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace core.Models
{
    public class GenericModel
    {
    }

    [DataContract]
    public class AreaModel
    {
        [DataMember]
        public int IdArea { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public string NombreArea { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Encargado { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }
    [DataContract]
    public class ProcesoModel
    {
        [DataMember]
        public int IdProceso { get; set; }
        [DataMember]
        public int IdArea { get; set; }
        [DataMember]
        public string NombreProceso { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Nullable<int> Encargado { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }
    [DataContract]
    public class SubProcesoModel
    {
        [DataMember]
        public int IdSubProceso { get; set; }
        [DataMember]
        public int IdProceso { get; set; }
        [DataMember]
        public string NombreSubProceso { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Nullable<int> Encargado { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }

    [DataContract]
    public class EmpresaModel
    {
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public string NombreEmpresa { get; set; }
        [DataMember]
        public string Calle { get; set; }
        [DataMember]
        public string NoExterior { get; set; }
        [DataMember]
        public string NoInterior { get; set; }
        [DataMember]
        public int CodPostal { get; set; }
        [DataMember]
        public string Colonia { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }

    [DataContract]
    public class TrabajadorModel
    {
        [DataMember]
        public int IdTrabajador { get; set; }
        [DataMember]

        public string IdPuesto { get; set; }
        [DataMember]

        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Calle { get; set; }
        [DataMember]
        public string NroExterior { get; set; }
        [DataMember]
        public string NroInterior { get; set; }
        [DataMember]
        public string Colonia { get; set; }
        [DataMember]
        public int CodigoPostal { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        [DataMember]
        public int NroSeguro { get; set; }
        [DataMember]
        public string RFC { get; set; }
        [DataMember]
        public string NombreContactoEmergencia { get; set; }
        [DataMember]
        public string TelefonoContactoEmergencia { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }


    [DataContract]
    public class IdTwointeger
    {
        [DataMember]
        public int Idheader { get; set; }
        [DataMember]
        public int Iddetail { get; set; }


    }

    [DataContract]
    public class MaterialModel
    {
        [DataMember]
        public string IdMaterial { get; set; }
        [DataMember]
        public int IdArea { get; set; }
        [DataMember]
        public int IdUnidadMedida { get; set; }
        [DataMember]
        public long NumeroParteMaterial { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Marca { get; set; }
        [DataMember]
        public string Posicion { get; set; }
        [DataMember]
        public double CostoUnidad { get; set; }
        [DataMember]
        public string NoSerie { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }

    [DataContract]
    public class GuiaServicioModel
    {
        [DataMember]
        public int IdGuiaServicio { get; set; }
        [DataMember]
        public int IdMarca { get; set; }
        [DataMember]
        public string Marca { get; set; }
        [DataMember]
        public int IdModelo { get; set; }
        [DataMember]
        public string Modelo { get; set; }
        [DataMember]
        public int IdArea { get; set; }
        [DataMember]
        public string area { get; set; }
        [DataMember]
        public int Km_Hr { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }

    }
    [DataContract]
    public class DetalleGuiaServicioModel
    {
        [DataMember]
        public int IdDetalleGuiaServicio { get; set; }
        [DataMember]
        public int IdGuiaServicio { get; set; }
        [DataMember]
        public string Actividad { get; set; }
        [DataMember]
        public double Tiempo { get; set; }
        [DataMember]
        public string Observaciones { get; set; }

    }
    [DataContract]
    public class DetalleGuiaServicioMaterialModel
    {
        [DataMember]
        public int IdDetalleGuiaServiciosMaterial { get; set; }
        [DataMember]
        public int IdGuiaServicio { get; set; }
        [DataMember]
        public int IdDetalleGuiaServicio { get; set; }
        [DataMember]
        public string IdMaterial { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }
    [DataContract]
    public class DetalleGuiaServicioRefaccionModel
    {
        [DataMember]
        public int IdDetalleGuiaServiciosRefaccion { get; set; }
        [DataMember]
        public int IdGuiaServicio { get; set; }
        [DataMember]
        public int IdDetalleGuiaServicio { get; set; }
        [DataMember]
        public string IdReFaccion { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }
    [DataContract]
    public class DetalleGuiaServicioPuestoModel
    {
        [DataMember]
        public int IdDetalleGuiaServiciosPuesto { get; set; }
        [DataMember]
        public int IdGuiaServicio { get; set; }
        [DataMember]
        public int IdDetalleGuiaServicio { get; set; }
        [DataMember]
        public string IdPuesto { get; set; }
        [DataMember]
        public double Cantidad { get; set; }

    }


    [DataContract]
    public class BackLogModel
    {
        [DataMember]
        public int IdBackLog { get; set; }
        [DataMember]
        public int IdUnidad { get; set; }
        //[DataMember]
        //public int IdArea { get; set; }
        [DataMember]
        public System.DateTime Fecha { get; set; }
        [DataMember]
        public string Actividad { get; set; }
        [DataMember]
        public double Tiempo { get; set; }
        [DataMember]
        public Nullable<bool> Ejecutada { get; set; }
    }
    [DataContract]
    public class BackLogMaterialModel
    {
        [DataMember]
        public int IdBackLogMaterial { get; set; }
        [DataMember]
        public int IdBackLog { get; set; }
        [DataMember]
        public string IdMaterial { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }
    [DataContract]
    public class BackLogRefactionModel
    {
        [DataMember]
        public int IdBackLogRefaccion { get; set; }
        [DataMember]
        public int IdBackLog { get; set; }
        [DataMember]
        public string IdReFaccion { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }
    [DataContract]
    public class BackLogJobModel
    {
        [DataMember]
        public int IdBackLogPuesto { get; set; }
        [DataMember]
        public int IdBackLog { get; set; }
        [DataMember]
        public string IdPuesto { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }

    [DataContract]
    public class PlanTrabajoModel
    {
        [DataMember]
        public int IdPlanTrabajo { get; set; }
        [DataMember]
        public int IdOrdenTrabajo { get; set; }
        [DataMember]
        public int IdDetalleOrdenTrabajo { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public string TipoServicio { get; set; }
        [DataMember]
        public string Actividad { get; set; }
        [DataMember]
        public double Tiempo { get; set; }
        [DataMember]
        public System.DateTime FechaPlanInicio { get; set; }
        [DataMember]
        public System.DateTime FechaPlanFin { get; set; }
        [DataMember]
        public System.TimeSpan Comienza { get; set; }
    }
    [DataContract]
    public class DetallePlanTrabajoModel
    {
        [DataMember]
        public int IdDetallePlanTrabajo { get; set; }
        [DataMember]
        public int IdPlanTrabajo { get; set; }
        [DataMember]
        public int IdTrabajador { get; set; }
    }

    [DataContract]
    public class UbicacionModel
    {
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public Nullable<int> IdEmpresa { get; set; }
        [DataMember]
        public string Calle { get; set; }
        [DataMember]
        public string NoExterior { get; set; }
        [DataMember]
        public string NoInterior { get; set; }
        [DataMember]
        public Nullable<int> CodPostal { get; set; }
        [DataMember]
        public string Colonia { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public Nullable<int> Gerente { get; set; }
        [DataMember]
        public Nullable<bool> Activo_Inactivo { get; set; }
    }

    [DataContract]
    public class UnidadMedidaModel
    {
        [DataMember]
        public int IdUnidadMedida { get; set; }
        [DataMember]
        public int IdUbicacion { get; set; }
        [DataMember]
        public string Nomenclatura { get; set; }
    }

    //////////////////////////////////
    /////////////////////////////////
    public class MantenimientoPredictivoDetailModel
    {
        [DataMember]
        public int IdDetalleMantenimientoPredictivo { get; set; }
        [DataMember]
        public int IdMantenimientoPredictivo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Unidad { get; set; }
        [DataMember]
        public double LimiteInf { get; set; }
        [DataMember]
        public double LimiteSup { get; set; }
    }
    public class ActividadMantenimientoPredictivoModel
    {
        [DataMember]
        public int IdActividadMantenimientoPredictivo { get; set; }
        [DataMember]
        public int IdDetalleMantenimientoPredictivo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public double Tiempo { get; set; }
        [DataMember]
        public Nullable<bool> Ejecutada { get; set; }
    }

    public class MantenimientoPredictivoMaterialModel
    {
        [DataMember]
        public int IdMantenimientoPredictivoMaterial { get; set; }
        [DataMember]
        public int IdActividadMantenimientoPredictivo { get; set; }
        [DataMember]
        public int IdDetalleMantenimientoPredictivo { get; set; }
        [DataMember]
        public string IdMaterial { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }

    [DataContract]
    public class MantenimientoPredictivoRefactionModel
    {
        public int IdMantenimientoPredictivoRefaccion { get; set; }
        [DataMember]
        public int IdActividadMantenimientoPredictivo { get; set; }
        [DataMember]
        public int IdDetalleMantenimientoPredictivo { get; set; }
        [DataMember]
        public string IdReFaccion { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }
    [DataContract]
    public class MantenimientoPredictivoJobModel
    {
        [DataMember]
        public int IdMantenimientoPredictivoPuesto { get; set; }
        [DataMember]
        public int IdActividadMantenimientoPredictivo { get; set; }
        [DataMember]
        public int IdDetalleMantenimientoPredictivo { get; set; }
        [DataMember]
        public string IdPuesto { get; set; }
        [DataMember]
        public double Cantidad { get; set; }
    }


}