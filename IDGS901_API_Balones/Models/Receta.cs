using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Receta
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ingredientesMateriaP { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int CantidadProducto { get; set; }
        public string Estatus { get; set; }
    }

}
