using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Componentes;

namespace MVC_ComponentesCodeFirst.ViewComponents.Componentes;

[ViewComponent]
public class CardComponenteViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ComponenteDto componente)
    {
        return View(componente);
    }
}
