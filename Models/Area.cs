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
    
    public partial class Area
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Area()
        {
            this.Alerta = new HashSet<Alerta>();
            this.CatalogoEvento = new HashSet<CatalogoEvento>();
            this.CategoriaUnidad = new HashSet<CategoriaUnidad>();
            this.DiaFestivo = new HashSet<DiaFestivo>();
            this.Elemento = new HashSet<Elemento>();
            this.EstadoGuiaInspeccion = new HashSet<EstadoGuiaInspeccion>();
            this.EstatusOrdenTrabajo = new HashSet<EstatusOrdenTrabajo>();
            this.GuiaInspeccion = new HashSet<GuiaInspeccion>();
            this.GuiaServicio = new HashSet<GuiaServicio>();
            this.MantenimientoPredictivo = new HashSet<MantenimientoPredictivo>();
            this.Marca = new HashSet<Marca>();
            this.Material = new HashSet<Material>();
            this.Operacion = new HashSet<Operacion>();
            this.OrdenTrabajo = new HashSet<OrdenTrabajo>();
            this.PlanTrabajo = new HashSet<PlanTrabajo>();
            this.Proceso = new HashSet<Proceso>();
            this.Puesto = new HashSet<Puesto>();
            this.Sistema = new HashSet<Sistema>();
            this.SistemaRefacciones = new HashSet<SistemaRefacciones>();
            this.TipoActividad = new HashSet<TipoActividad>();
            this.TipoContrato = new HashSet<TipoContrato>();
            this.TipoEvento = new HashSet<TipoEvento>();
            this.TrabajadorArea = new HashSet<TrabajadorArea>();
            this.Turno = new HashSet<Turno>();
            this.UnidadArea = new HashSet<UnidadArea>();
            this.UsuarioArea = new HashSet<UsuarioArea>();
        }
    
        public int IdArea { get; set; }
        public int IdUbicacion { get; set; }
        public string NombreArea { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Encargado { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alerta> Alerta { get; set; }
        public virtual Ubicacion Ubicacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogoEvento> CatalogoEvento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoriaUnidad> CategoriaUnidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaFestivo> DiaFestivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Elemento> Elemento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoGuiaInspeccion> EstadoGuiaInspeccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstatusOrdenTrabajo> EstatusOrdenTrabajo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuiaInspeccion> GuiaInspeccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuiaServicio> GuiaServicio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MantenimientoPredictivo> MantenimientoPredictivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Marca> Marca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Material> Material { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operacion> Operacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanTrabajo> PlanTrabajo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proceso> Proceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Puesto> Puesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sistema> Sistema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SistemaRefacciones> SistemaRefacciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoActividad> TipoActividad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoContrato> TipoContrato { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoEvento> TipoEvento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrabajadorArea> TrabajadorArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turno> Turno { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadArea> UnidadArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioArea> UsuarioArea { get; set; }
    }
}
