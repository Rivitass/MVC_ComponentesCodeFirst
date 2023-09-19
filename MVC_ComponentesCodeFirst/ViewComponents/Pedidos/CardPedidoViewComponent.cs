using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.ViewComponents.Pedidos;

[ViewComponent]
public class CardPedidoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PedidoDto pedido)
    {
        return View(pedido);
    }
}
