using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.ViewComponents.Ordenadores;

[ViewComponent]
public class CardOrdenadorViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(OrdenadorDto ordenador)
    {
        return View(ordenador);
    }
}
