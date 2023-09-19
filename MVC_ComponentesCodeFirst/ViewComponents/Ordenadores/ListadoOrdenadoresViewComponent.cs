using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.ViewComponents.Ordenadores;

[ViewComponent]
public class ListadoOrdenadoresViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<OrdenadorDto> ordenadores)
    {
        return View(ordenadores);
    }
}
