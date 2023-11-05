using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
