using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Ordenadores;

public class EfOrdenadorRepository : IOrdenadorRepository
{
    private readonly ComponenteContext _dbContext;
    private readonly ILoggerManager _logger;

    public EfOrdenadorRepository(ComponenteContext dbContext, ILoggerManager logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<List<OrdenadorDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de ordenadores");

        return Task.FromResult(_dbContext.Ordenadores.Include("Componentes").Include("Pedido").AsEnumerable().Select(OrdenadorToDto.ConvertWithoutIncludeComponentes).ToList());
    }

    public Task<OrdenadorDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el ordenador con id = {id}");

        var ordenador = Task.FromResult(_dbContext.Ordenadores
            .Include("Componentes")
            .Include("Pedido")
            .AsEnumerable()
            .Select(OrdenadorToDto.Convert).SingleOrDefault(ordenador => ordenador.Id.Equals(id)));

        return ordenador;
    }

    public async Task AddAsync(OrdenadorDto ordenador)
    {
        _logger.LogInfo($"Ordenador con id = {ordenador.Id} añadido");

        _dbContext.Ordenadores.Add(new Ordenador()
        {
            Id = ordenador.Id,
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrdenadorDto ordenador)
    {
        _logger.LogInfo($"Ordenador con id = {ordenador.Id} actualizado");

        _dbContext.Ordenadores.Update(new Ordenador()
        {
            Id = ordenador.Id,
            Descripcion = ordenador.Descripcion,
            PedidoId = ordenador.PedidoId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Ordenador con id = {id} eliminado");

        _dbContext.Ordenadores.Remove(new Ordenador()
        {
            Id = id,
        });

        await _dbContext.SaveChangesAsync();
    }
}
