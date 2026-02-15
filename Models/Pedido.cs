namespace PosPizza.Models
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int DireccionId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Total { get; set; }
        public string? Estado { get; set; }
    }

    public class PedidoDetailDTO : PedidoDTO
    {
        public new ClienteDTO? Cliente { get; set; }
        public new DireccionDTO? Direccion { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public List<PedidoDetalleDetailDTO> Detalles { get; set; } = new();
    }

    public class PedidoCreateUpdateDTO
    {
        public int ClienteId { get; set; }
        public int DireccionId { get; set; }
        public int UsuarioId { get; set; }
        public string? Estado { get; set; }
        public List<PedidoDetalleCreateUpdateDTO> Detalles { get; set; } = new();
    }

    public class PedidoDetalleDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class PedidoDetalleDetailDTO : PedidoDetalleDTO
    {
        public ProductoDTO? Producto { get; set; }
    }

    public class PedidoDetalleCreateUpdateDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}