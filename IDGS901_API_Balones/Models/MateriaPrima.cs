using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public string? Unidad_Medida { get; set; }
        public int? Stock { get; set; }
        public string? Estatus { get; set; }
    }
}
