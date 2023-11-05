using IDGS901_API_Balones.Models;
using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Model
{
    public class DetalleCompraMateria
    {
        [Key]
        public int id_detalle_compra { get; set; }
        public CompraMateriaP compra_materia { get; set; }
        public MateriaPrima materia { get; set; }
        public int cantidad { get; set; }
        public Decimal costo_compra { get; set; }

    }
}
