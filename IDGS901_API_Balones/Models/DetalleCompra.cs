using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }
        public int Id_materia_prima { get; set; }
        public int Cantidad { get; set; }
        public int Costo_compra { get; set; }
    }
}
