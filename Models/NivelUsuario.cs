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
    
    public partial class NivelUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NivelUsuario()
        {
            this.AlertaNivelUsuario = new HashSet<AlertaNivelUsuario>();
            this.OperacioneNivelUsuario = new HashSet<OperacioneNivelUsuario>();
        }
    
        public int IdNivelUsuario { get; set; }
        public string NombreNivel { get; set; }
        public string DescripcionNivel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertaNivelUsuario> AlertaNivelUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperacioneNivelUsuario> OperacioneNivelUsuario { get; set; }
    }
}
