using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Pedidos;

public class EfPedidoRepository : IPedidoRepository
{
    private readonly ComponenteContext _dbContext;
    private readonly ILoggerManager _logger;

    public EfPedidoRepository(ComponenteContext dbContext, ILoggerManager logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<List<PedidoDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de pedidos");

        var pedidos = _dbContext.Pedidos
            .Include(pedido => pedido.Ordenadores)
            .ThenInclude(ordenador => ordenador.Componentes)
            .AsEnumerable()
            .Select(PedidoToDto.ConvertWithoutIncludeOrdenadores)
            .ToList();

        return Task.FromResult(pedidos);
    }

    public Task<PedidoDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el pedido con id = {id}");

        var pedido = _dbContext.Pedidos
            .Include(pedido => pedido.Ordenadores)
            .ThenInclude(ordenador => ordenador.Componentes)
            .AsEnumerable()
            .Select(PedidoToDto.Convert)
            .SingleOrDefault(pedido => pedido.Id.Equals(id));

        return Task.FromResult(pedido);
    }

    public async Task AddAsync(PedidoDto pedido)
    {
        _logger.LogInfo($"Pedido con id = {pedido.Id} añadido");

        _dbContext.Pedidos.Add(new Pedido()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(ordenador => new Ordenador()
            {
                Id = ordenador.Id,
                Descripcion = ordenador.Descripcion,
                PedidoId = ordenador.PedidoId
            }).ToList()
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(PedidoDto pedido)
    {
        _logger.LogInfo($"Pedido con id = {pedido.Id} actualizado");

        _dbContext.Pedidos.Update(new Pedido()
        {
            Id = pedido.Id,
            Descripcion = pedido.Descripcion,
            Fecha = pedido.Fecha,
            Ordenadores = pedido.Ordenadores.Select(ordenador => new Ordenador()
            {
                Id = ordenador.Id,
                Descripcion = ordenador.Descripcion,
                PedidoId = ordenador.PedidoId
            }).ToList()
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Pedido con id = {id} eliminado");

        _dbContext.Pedidos.Remove(new Pedido()
        {
            Id = id
        });

        await _dbContext.SaveChangesAsync();
    }
}
