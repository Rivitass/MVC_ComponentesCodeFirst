using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Componentes;

public class EfComponenteRepository : IComponenteRepository
{
    private readonly ComponenteContext _dbContext;
    private readonly ILoggerManager _logger;

    public EfComponenteRepository(ComponenteContext dbContext, ILoggerManager logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<List<ComponenteDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de componentes");

        return Task.FromResult(_dbContext.Componentes.Include("Ordenador").AsEnumerable().Select(ComponenteToDto.Convert).ToList());
    }

    public Task<ComponenteDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el componente con id = {id}");

        return Task.FromResult(_dbContext.Componentes.Include("Ordenador").AsEnumerable().Select(ComponenteToDto.Convert).ToList().FirstOrDefault(componente => componente.Id.Equals(id)));
    }

    public async Task AddAsync(ComponenteDto componente)
    {
        _logger.LogInfo($"Componente con id = {componente.Id} añadido");

        _dbContext.Componentes.Add(new Componente()
        {
            Id = componente.Id,
            NumeroDeSerie = componente.NumeroDeSerie,
            Descripcion = componente.Descripcion,
            Calor = componente.Calor,
            Megas = componente.Megas,
            Cores = componente.Cores,
            Coste = componente.Coste,
            Tipo = componente.Tipo,
            OrdenadorId = componente.OrdenadorId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ComponenteDto componente)
    {
        _logger.LogInfo($"Componente con id = {componente.Id} actualizado");

        _dbContext.Componentes.Update(new Componente()
        {
            Id = componente.Id,
            NumeroDeSerie = componente.NumeroDeSerie,
            Descripcion = componente.Descripcion,
            Calor = componente.Calor,
            Megas = componente.Megas,
            Cores = componente.Cores,
            Coste = componente.Coste,
            Tipo = componente.Tipo,
            OrdenadorId = componente.OrdenadorId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Componente con id = {id} eliminado");

        _dbContext.Componentes.Remove(new Componente()
        {
            Id = id,
        });

        await _dbContext.SaveChangesAsync();
    }
}
