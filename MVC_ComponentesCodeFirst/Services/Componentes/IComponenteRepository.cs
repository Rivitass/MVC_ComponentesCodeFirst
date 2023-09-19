using MVC_ComponentesCodeFirst.Models.Componentes;

namespace MVC_ComponentesCodeFirst.Services.Componentes;

public interface IComponenteRepository
{
    Task<List<ComponenteDto>> AllAsync();

    Task<ComponenteDto?> GetByIdAsync(int id);

    Task AddAsync(ComponenteDto componente);

    Task UpdateAsync(ComponenteDto componente);

    Task DeleteAsync(int id);
}
