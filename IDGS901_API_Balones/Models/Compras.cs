using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Compras
    {
        [Key]
        public int Id_compra_materia { get; set; }
        public DateTime Fecha { get; set; }
        public int IdProveedor { get; set; }
        //public string NombreProveedor { get; set; }
        //public string NombreMateriaPrima { get; set; }
        public int Id_materia_prima { get; set; }
        public int Cantidad { get; set; }
        public int Costo_compra { get; set; }
        public string Estatus { get; set; }
    }


}
