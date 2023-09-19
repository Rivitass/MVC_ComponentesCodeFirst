using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Models.Utils;

public static class OrdenadorToDto
{
    public static OrdenadorDto Convert(Ordenador ordenador)
    {
        return new OrdenadorDto()
        {
            Id = ordenador.Id,
            Componentes = ordenador.Componentes.Select(ComponenteToDto.ConvertWithoutIncludeOrdenador).ToList(),
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId,
            Pedido = PedidoToDto.ConvertWithoutIncludeOrdenadores(ordenador.Pedido),
            Coste = ordenador.Componentes.Sum(componente => componente.Coste)
        };
    }

    public static OrdenadorDto ConvertWithoutIncludeComponentes(Ordenador ordenador)
    {
        return new OrdenadorDto()
        {
            Id = ordenador.Id,
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId,
            Pedido = PedidoToDto.ConvertWithoutIncludeOrdenadores(ordenador.Pedido),
            Coste = ordenador.Componentes.Sum(componente => componente.Coste)
        };
    }
}
