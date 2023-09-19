using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Componentes;

namespace MVC_ComponentesCodeFirst.ViewComponents.Componentes;

[ViewComponent]
public class ListadoComponentesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<ComponenteDto> componentes)
    {
        return View(componentes);
    }
}
