using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Direccion
    {
        [Key]
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string CalleNumero { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public int IdCliente { get; set; }
    }

}
