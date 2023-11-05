using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public string? Imagen { get; set; }

        public string? Descripcion { get; set; }

        public int Precio { get; set; }

        public int? Stock { get; set; }

        public string? Estatus { get; set; }


    }

}
