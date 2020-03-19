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
    
    public partial class GuiaInspeccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GuiaInspeccion()
        {
            this.EventosGuiaInspeccion = new HashSet<EventosGuiaInspeccion>();
            this.IndicadorGuiaInspeccion = new HashSet<IndicadorGuiaInspeccion>();
            this.InsumosGuiaInspeccion = new HashSet<InsumosGuiaInspeccion>();
        }
    
        public int IdGuiaInspeccion { get; set; }
        public int IdUnidad { get; set; }
        public int IdArea { get; set; }
        public int IdTrabajador { get; set; }
        public int IdTurno { get; set; }
        public int KilometrajeInicial { get; set; }
        public int KilometrajeFinal { get; set; }
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public double Produccion { get; set; }
        public string Observaciones { get; set; }
        public System.DateTime FechaHora_GuiaInpeccion { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        public virtual Area Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventosGuiaInspeccion> EventosGuiaInspeccion { get; set; }
        public virtual Trabajador Trabajador { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Unidad Unidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndicadorGuiaInspeccion> IndicadorGuiaInspeccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsumosGuiaInspeccion> InsumosGuiaInspeccion { get; set; }
    }
}
