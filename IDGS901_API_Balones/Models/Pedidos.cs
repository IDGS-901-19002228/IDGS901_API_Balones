using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Pedidos
    {
        [Key]
        public int idPedido { get; set; }
        public Clientes cliente { get; set; }
        public Direccion direccion { get; set; }
        public Tarjetas tarjeta { get; set; }
        public string folio { get; set; }
        public DateTime fecha { get; set; }
        public string estatus { get; set; }
    }
}
