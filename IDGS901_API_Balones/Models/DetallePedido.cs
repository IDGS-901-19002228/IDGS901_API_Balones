namespace IDGS901_API_Balones.Models
{
    public class DetallePedido
    {
        public int id { get; set; }
        public int cantidad { get; set; }
        public Pedidos? pedidos { get; set; }
        public Productos? productos { get; set; }
    }
}
