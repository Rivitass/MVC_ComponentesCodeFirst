using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.ViewComponents.Pedidos;

[ViewComponent]
public class ListadoPedidosViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<PedidoDto> pedidos)
    {
        return View(pedidos);
    }
}
