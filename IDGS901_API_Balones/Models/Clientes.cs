using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    //public class Clientes
    //{
    //    [Key]
    //    public int? Id { get; set; }
    //    public string? Nombre { get; set; }
    //    public string? Usuario { get; set; }
    //    public string? Correo { get; set; }
    //    public string? Contrasenia { get; set; }
    //    public string? Rol { get; set; }
    //    public string? Estatus { get; set; }
    //}

    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenia { get; set; }
        public string? Rol { get; set; }
        public string? Estatus { get; set; }
    }
}
