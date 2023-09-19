using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.Services.Pedidos;

public interface IPedidoRepository
{
    Task<List<PedidoDto>> AllAsync();

    Task<PedidoDto?> GetByIdAsync(int id);

    Task AddAsync(PedidoDto pedido);

    Task UpdateAsync(PedidoDto pedido);

    Task DeleteAsync(int id);
}
