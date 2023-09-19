using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Services.Ordenadores;

public interface IOrdenadorRepository
{
    Task<List<OrdenadorDto>> AllAsync();

    Task<OrdenadorDto?> GetByIdAsync(int id);

    Task AddAsync(OrdenadorDto ordenador);

    Task UpdateAsync(OrdenadorDto ordenador);

    Task DeleteAsync(int id);
}
