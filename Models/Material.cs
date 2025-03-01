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
    
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            this.BackLogMaterial = new HashSet<BackLogMaterial>();
            this.DetalleGuiaServiciosMaterial = new HashSet<DetalleGuiaServiciosMaterial>();
            this.DetalleOrdenTrabajo = new HashSet<DetalleOrdenTrabajo>();
            this.InsumosGuiaInspeccion = new HashSet<InsumosGuiaInspeccion>();
            this.MantenimientoPredictivoMaterial = new HashSet<MantenimientoPredictivoMaterial>();
        }
    
        public string IdMaterial { get; set; }
        public int IdArea { get; set; }
        public int IdUnidadMedida { get; set; }
        public Nullable<long> NumeroParteMaterial { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Posicion { get; set; }
        public double CostoUnidad { get; set; }
        public string NoSerie { get; set; }
        public int Cantidad { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        public virtual Area Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BackLogMaterial> BackLogMaterial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleGuiaServiciosMaterial> DetalleGuiaServiciosMaterial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOrdenTrabajo> DetalleOrdenTrabajo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsumosGuiaInspeccion> InsumosGuiaInspeccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MantenimientoPredictivoMaterial> MantenimientoPredictivoMaterial { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
    }
}
