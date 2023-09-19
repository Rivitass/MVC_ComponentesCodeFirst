using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Ordenadores;

public class FakeOrdenadorRepository : IOrdenadorRepository
{
    private readonly List<Ordenador> _ordenadores = new()
    {
        new Ordenador { Id = 1, Descripcion = "OrdenadorPrueba" }
    };

    public Task<List<OrdenadorDto>> AllAsync()
    {
        return Task.FromResult(_ordenadores.Select(ordenador =>
            new OrdenadorDto()
            {
                Id = ordenador.Id,
                Componentes = ordenador.Componentes.Select(ComponenteToDto.Convert).ToList(),
                Descripcion = ordenador.Descripcion,
                Coste = ordenador.Componentes.Sum(componente => componente.Coste),
                PedidoId = ordenador.PedidoId,
                Pedido = PedidoToDto.Convert(ordenador.Pedido)
            }).ToList());
    }

    public Task<OrdenadorDto?> GetByIdAsync(int id)
    {
        return Task.FromResult(_ordenadores.Select(ordenador => new OrdenadorDto()
        {
            Id = ordenador.Id,
            Componentes = ordenador.Componentes.Select(ComponenteToDto.Convert).ToList(),
            Descripcion = ordenador.Descripcion,
            Coste = ordenador.Componentes.Sum(componente => componente.Coste),
            PedidoId = ordenador.PedidoId,
            Pedido = PedidoToDto.Convert(ordenador.Pedido)
        }).FirstOrDefault(ordenador => ordenador.Id == id));
    }

    public Task AddAsync(OrdenadorDto ordenador)
    {
        int nuevoId = _ordenadores.Count + 1;
        ordenador.Id = nuevoId;

        _ordenadores.Add(new Ordenador()
        {
            Id = ordenador.Id,
            // Componentes = ordenador.Componentes,
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId
            // Pedido = ordenador.Pedido
        });

        return Task.CompletedTask;
    }

    public Task UpdateAsync(OrdenadorDto ordenador)
    {
        var ordenadorActualizado = _ordenadores.Find(element => element.Id.Equals(ordenador.Id));

        if (ordenadorActualizado is null) return Task.CompletedTask;

        ordenadorActualizado.Id = ordenador.Id;
        // ordenadorActualizado.Componentes = ordenador.Componentes.ToList();
        ordenadorActualizado.Descripcion = ordenador.Descripcion;
        ordenadorActualizado.PedidoId = ordenador.PedidoId;
        // ordenadorActualizado.Pedido = ordenador.Pedido;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Ordenador? ordenador = _ordenadores.Find(ordenador => ordenador.Id == id);

        if (ordenador != null)
        {
            _ordenadores.Remove(ordenador);
        }

        return Task.CompletedTask;
    }
}
