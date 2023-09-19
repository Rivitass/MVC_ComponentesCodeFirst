using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Models.Pedidos;

public class PedidoDto
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    public ICollection<OrdenadorDto> Ordenadores { get; set; } = new List<OrdenadorDto>();

    public int Coste { get; set; }
}
