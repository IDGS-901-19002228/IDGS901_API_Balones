using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Tarjetas
    {
        [Key]
        public int Id { get; set; }

        public string NombreTarjeta { get; set; }

        public string NumTarjeta { get; set; }

        public string FechaVencimiento { get; set; }

        public string CCV { get; set; }
        //public string Estatus { get; set; }
        public Clientes Usuario { get; set; }

    }
}
