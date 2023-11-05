namespace IDGS901_API_Balones.Models
{
    public class Venta
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string estatus { get; set; }
        public string cliente { get; set; }
        public string nombreproducto { get; set; }
        public int cantidad { get; set; }
        public int precioUnitario { get; set; }
    }
}
