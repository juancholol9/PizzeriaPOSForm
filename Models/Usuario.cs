namespace PosPizza.Models
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }

    public class UsuarioCreateUpdateDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Empleado";
        public bool? Activo { get; set; } = true;
    }
}