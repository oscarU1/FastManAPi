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
    
    public partial class Operacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operacion()
        {
            this.OperacioneNivelUsuario = new HashSet<OperacioneNivelUsuario>();
        }
    
        public int IdOperacion { get; set; }
        public int IdArea { get; set; }
        public string CodigoOperacion { get; set; }
        public string NombreOperacion { get; set; }
    
        public virtual Area Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperacioneNivelUsuario> OperacioneNivelUsuario { get; set; }
    }
}
