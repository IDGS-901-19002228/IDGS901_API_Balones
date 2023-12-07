﻿using System.ComponentModel.DataAnnotations;

namespace IDGS901_API_Balones.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public string? Sexo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Rol { get; set; }
        public string? Estatus { get; set; }
    }


}
