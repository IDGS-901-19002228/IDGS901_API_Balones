using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Proveedor
    {
        [Key]
        public int? id { get; set; }
        public string? nombre { get; set; }
        public string? empresa { get; set; }
        public string? rfc { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; }
        public string? estatus { get; set; }
    }

}