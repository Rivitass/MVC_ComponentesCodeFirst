using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.Models.Ordenadores;

public class OrdenadorDto
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public ICollection<ComponenteDto> Componentes { get; set; } = new List<ComponenteDto>();

    public int? PedidoId { get; set; }

    public PedidoDto? Pedido { get; set; }

    public int Coste { get; set; }
}
