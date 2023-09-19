using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Services.Componentes;
using MVC_ComponentesCodeFirst.Services.Ordenadores;

namespace MVC_ComponentesCodeFirst.Controllers;

public class ComponentesController : Controller
{
    private readonly IComponenteRepository _componenteRepository;
    private readonly IOrdenadorRepository _ordenadorRepository;
    private readonly ILoggerManager _logger;

    public ComponentesController(IComponenteRepository componenteRepository, IOrdenadorRepository ordenadorRepository, ILoggerManager logger)
    {
        _componenteRepository = componenteRepository;
        _ordenadorRepository = ordenadorRepository;
        _logger = logger;
    }

    // GET: Componentes
    public async Task<IActionResult> Index()
    {
        _logger.LogInfo("Se va a mostrar la lista de componentes");

        return View("Index", await _componenteRepository.AllAsync());
    }

    // GET: Componentes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        _logger.LogInfo($"Se va a mostrar los detalles del componente con id = {id}");

        if (id == null) return NotFound();
        
        var componente = await _componenteRepository.GetByIdAsync((int)id);

        if (componente == null) return NotFound();
                
        return View("Details", componente);
    }

    // GET: Componentes/Create
    public async Task<IActionResult> Create()
    {
        _logger.LogInfo("Se va a mostrar la vista para crear un componente");

        var ordenadores = await _ordenadorRepository.AllAsync();

        ViewBag.Ordenadores = ordenadores.Select(ordenador => new SelectListItem()
        {
            Value = ordenador.Id.ToString(),
            Text = ordenador.Descripcion
        });

        return View("Create");
    }

    // POST: Componentes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComponenteDto componente)
    {
        _logger.LogInfo("Se va a crear un componente");

        if (!ModelState.IsValid) return View(componente);

        await _componenteRepository.AddAsync(componente);

        return RedirectToAction(nameof(Index));
    }

    // GET: Componentes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para editar el componente con id = {id}");

        if (id == null) return NotFound();

        var componente = await _componenteRepository.GetByIdAsync((int)id);
        var ordenadores = await _ordenadorRepository.AllAsync();

        ViewBag.Ordenadores = ordenadores.Select(ordenador => new SelectListItem()
        {
            Value = ordenador.Id.ToString(), 
            Text = ordenador.Descripcion
        });
        
        if (componente == null) return NotFound();

        return View(componente);
    }

    // POST: Componentes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ComponenteDto componente)
    {
        _logger.LogInfo($"Se va a editar el componente con id = {id}");

        if (id != componente.Id) return NotFound();

        foreach (var keyModelStatePair in ModelState)
        {
            var key = keyModelStatePair.Key;
            var errors = keyModelStatePair.Value.Errors;
        }

        if (!ModelState.IsValid) return View(componente);

        try
        {
            await _componenteRepository.UpdateAsync(componente);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ComponenteExists(componente.Id)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Componentes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para eliminar el componente con id = {id}");

        if (id == null) return NotFound();

        var componente = await _componenteRepository.GetByIdAsync((int)id);

        if (componente == null) return NotFound();

        return View(componente);
    }

    // POST: Componentes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _logger.LogInfo($"Se va a eliminar el componente con id = {id}");

        await _componenteRepository.DeleteAsync((int)id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ComponenteExists(int id)
    {
        var componente = await _componenteRepository.GetByIdAsync(id);

        return componente != null;
    }
}
