using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Pedidos;

public class FakePedidoRepository : IPedidoRepository
{
    private readonly List<Pedido> _pedidos = new()
    {
        new Pedido()
        {
            Id = 1,
            Descripcion = "Pedido de prueba 1",
            Fecha = new DateTime(2023, 8, 25),
            Ordenadores = new List<Ordenador>()
        }
    };

    public Task<List<PedidoDto>> AllAsync()
    {
        return Task.FromResult(_pedidos.Select(pedido => new PedidoDto()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(ordenador => new OrdenadorDto()
            {
                Id = ordenador.Id,
                Componentes = ordenador.Componentes.Select(ComponenteToDto.Convert).ToList(),
                Descripcion = ordenador.Descripcion,
                Coste = ordenador.Componentes.Sum(componente => componente.Coste),
                PedidoId = ordenador.PedidoId,
                Pedido = PedidoToDto.Convert(ordenador.Pedido)
            }).ToList(),
            Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Componentes.Sum(componente => componente.Coste))
        }).ToList());
    }

    public Task<PedidoDto?> GetByIdAsync(int id)
    {
        return Task.FromResult(_pedidos.Select(pedido => new PedidoDto()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(ordenador => new OrdenadorDto()
            {
                Id = ordenador.Id,
                Componentes = ordenador.Componentes.Select(ComponenteToDto.Convert).ToList(),
                Descripcion = ordenador.Descripcion,
                Coste = ordenador.Componentes.Sum(componente => componente.Coste),
                PedidoId = ordenador.PedidoId,
                Pedido = PedidoToDto.Convert(ordenador.Pedido)
            }).ToList(),
            Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Componentes.Sum(componente => componente.Coste))
        }).FirstOrDefault(pedido => pedido.Id == id));
    }

    public Task AddAsync(PedidoDto pedido)
    {
        int nuevoId = _pedidos.Count + 1;
        pedido.Id = nuevoId;

        _pedidos.Add(new Pedido()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(ordenador => new Ordenador()
            {
                Id = ordenador.Id,
                // Componentes = ordenador.Componentes,
                Descripcion = ordenador.Descripcion,
                PedidoId = ordenador.PedidoId
                // Pedido = ordenador.Pedido
            }).ToList()
        });

        return Task.CompletedTask;
    }

    public Task UpdateAsync(PedidoDto pedido)
    {
        var pedidoActualizado = _pedidos.Find(element => element.Id.Equals(pedido.Id));

        if (pedidoActualizado is null) return Task.CompletedTask;

        pedidoActualizado.Id = pedido.Id;
        pedidoActualizado.Descripcion = pedido.Descripcion;
        pedidoActualizado.Fecha = pedido.Fecha;
        pedidoActualizado.Ordenadores = pedido.Ordenadores.Select(ordenador => new Ordenador()
        {
            Id = ordenador.Id,
            // Componentes = ordenador.Componentes,
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId
            // Pedido = ordenador.Pedido
        }).ToList();

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Pedido? pedido = _pedidos.Find(pedido => pedido.Id == id);

        if (pedido != null)
        {
            _pedidos.Remove(pedido);
        }

        return Task.CompletedTask;
    }
}
