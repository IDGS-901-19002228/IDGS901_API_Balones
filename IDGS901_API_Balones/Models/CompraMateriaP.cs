using IDGS901_API_Balones.Models;
using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Model
{
    public class CompraMateriaP
    {
        [Key]
        public int id_compra_materia { get; set; }
        public Usuarios empleado { get; set; }
        public Proveedor proveedor { get; set; }
        public  DateTime? fecha { get; set; }

    }
}
