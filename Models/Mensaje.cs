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
    
    public partial class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdUbicacion { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Activo_Inactivo { get; set; }
    
        public virtual Ubicacion Ubicacion { get; set; }
    }
}
