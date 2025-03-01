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
    
    public partial class SubProceso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubProceso()
        {
            this.TrabajadorSubProceso = new HashSet<TrabajadorSubProceso>();
            this.UnidadSubProceso = new HashSet<UnidadSubProceso>();
            this.UsuarioSubProceso = new HashSet<UsuarioSubProceso>();
        }
    
        public int IdSubProceso { get; set; }
        public int IdProceso { get; set; }
        public string NombreSubProceso { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Encargado { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        public virtual Proceso Proceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrabajadorSubProceso> TrabajadorSubProceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadSubProceso> UnidadSubProceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioSubProceso> UsuarioSubProceso { get; set; }
    }
}
