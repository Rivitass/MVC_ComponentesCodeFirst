using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Pedidos;

namespace MVC_ComponentesCodeFirst.Controllers;

public class OrdenadoresController : Controller
{
    private readonly IOrdenadorRepository _ordenadorRepository;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ILoggerManager _logger;

    public OrdenadoresController(IOrdenadorRepository ordenadorRepository, IPedidoRepository pedidoRepository, ILoggerManager logger)
    {
        _ordenadorRepository = ordenadorRepository;
        _pedidoRepository = pedidoRepository;
        _logger = logger;
    }

    // GET: Ordenadores
    public async Task<IActionResult> Index()
    {
        _logger.LogInfo("Se va a mostrar la lista de ordenadores");

        return View("Index", await _ordenadorRepository.AllAsync());
    }

    // GET: Ordenadores/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        _logger.LogInfo($"Se va a mostrar los detalles del ordenador con id = {id}");

        if (id == null) return NotFound();

        var ordenador = await _ordenadorRepository.GetByIdAsync((int)id);

        if (ordenador == null) return NotFound();

        return View("Details", ordenador);
    }

    // GET: Ordenadores/Create
    public async Task<IActionResult> Create()
    {
        _logger.LogInfo("Se va a mostrar la vista para crear un ordenador");

        var pedidos = await _pedidoRepository.AllAsync();

        ViewBag.Pedidos = pedidos.Select(pedido => new SelectListItem()
        {
            Value = pedido.Id.ToString(),
            Text = pedido.Descripcion
        });

        return View("Create");
    }

    // POST: Ordenadores/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrdenadorDto ordenador)
    {
        _logger.LogInfo("Se va a crear un ordenador");

        if (!ModelState.IsValid) return View(ordenador);

        await _ordenadorRepository.AddAsync(ordenador);

        return RedirectToAction(nameof(Index));
    }

    // GET: Ordenadores/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para editar el ordenador con id = {id}");

        if (id == null) return NotFound();

        var ordenador = await _ordenadorRepository.GetByIdAsync((int)id);
        var pedidos = await _pedidoRepository.AllAsync();

        ViewBag.Pedidos = pedidos.Select(pedido => new SelectListItem()
        {
            Value = pedido.Id.ToString(),
            Text = pedido.Descripcion
        });

        if (ordenador == null) return NotFound();

        return View(ordenador);
    }

    // POST: Ordenadores/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OrdenadorDto ordenador)
    {
        _logger.LogInfo($"Se va a editar el ordenador con id = {id}");

        if (id != ordenador.Id) return NotFound();

        if (!ModelState.IsValid) return View(ordenador);

        try
        {
            await _ordenadorRepository.UpdateAsync(ordenador);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await OrdenadorExists(ordenador.Id)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Ordenadores/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para eliminar el ordenador con id = {id}");

        if (id == null) return NotFound();

        var ordenador = await _ordenadorRepository.GetByIdAsync((int)id);

        if (ordenador == null) return NotFound();

        return View(ordenador);
    }

    // POST: Ordenadores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _logger.LogInfo($"Se va a eliminar el ordenador con id = {id}");

        await _ordenadorRepository.DeleteAsync((int)id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> OrdenadorExists(int id)
    {
        var ordenador = await _ordenadorRepository.GetByIdAsync(id);

        return ordenador != null;
    }
}
