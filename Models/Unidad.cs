//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace core.Models
{
    using System; using Newtonsoft.Json;
    using System.Collections.Generic;
    
    public partial class Unidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidad()
        {
            this.BackLog = new HashSet<BackLog>();
            this.CaracteristicasUnidad = new HashSet<CaracteristicasUnidad>();
            this.GuiaInspeccion = new HashSet<GuiaInspeccion>();
            this.MantenimientoPredictivo = new HashSet<MantenimientoPredictivo>();
            this.OrdenTrabajo = new HashSet<OrdenTrabajo>();
            this.UnidadArea = new HashSet<UnidadArea>();
            this.UnidadProceso = new HashSet<UnidadProceso>();
            this.UnidadSubProceso = new HashSet<UnidadSubProceso>();
            this.UnidadUbicacion = new HashSet<UnidadUbicacion>();
        }
    
        public int IdUnidad { get; set; }
        public int IdCategoriaUnidad { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int Encargado { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> Año { get; set; }
        public string Color { get; set; }
        public string Dimensiones { get; set; }
        public string Matricula { get; set; }
        public byte[] ImagenVehiculo { get; set; }
        public string NumeroChasis { get; set; }
        public int PeriodoServicio { get; set; }
        public double PrecioCompra { get; set; }
        public double MontoModificacion { get; set; }
        public double MontoRecuperacion { get; set; }
        public Nullable<bool> Km_Hr { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BackLog> BackLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaracteristicasUnidad> CaracteristicasUnidad { get; set; }
        public virtual CategoriaUnidad CategoriaUnidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuiaInspeccion> GuiaInspeccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MantenimientoPredictivo> MantenimientoPredictivo { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Modelo Modelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadArea> UnidadArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadProceso> UnidadProceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadSubProceso> UnidadSubProceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadUbicacion> UnidadUbicacion { get; set; }
    }
}
