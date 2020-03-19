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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.UsuarioArea = new HashSet<UsuarioArea>();
            this.UsuarioProceso = new HashSet<UsuarioProceso>();
            this.UsuarioSubProceso = new HashSet<UsuarioSubProceso>();
        }
    
        public int IdUsuario { get; set; }
        public int IdNivelUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña_Usuario { get; set; }
        public string CorreoElectronicoUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioArea> UsuarioArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioProceso> UsuarioProceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioSubProceso> UsuarioSubProceso { get; set; }
    }
}
