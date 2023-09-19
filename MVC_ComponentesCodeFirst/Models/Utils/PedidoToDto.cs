using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.Models.Utils;

public static class PedidoToDto
{

    public static PedidoDto? Convert(Pedido? pedido)
    {
        if (pedido == null) return null;

        return new PedidoDto()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(OrdenadorToDto.ConvertWithoutIncludeComponentes).ToList(),
            Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Componentes.Sum(componente => componente.Coste))
        };
    }

    public static PedidoDto? ConvertWithoutIncludeOrdenadores(Pedido? pedido)
    {
        if (pedido == null) return null;

        return new PedidoDto()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Componentes.Sum(componente => componente.Coste))
        };
    }
}

