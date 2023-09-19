using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Services.Pedidos;

namespace MVC_ComponentesCodeFirst.Controllers;

public class PedidosController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ILoggerManager _logger;

    public PedidosController(IPedidoRepository pedidoRepository, ILoggerManager logger)
    {
        _pedidoRepository = pedidoRepository;
        _logger = logger;
    }

    // GET: Pedidos
    public async Task<IActionResult> Index()
    {
        _logger.LogInfo("Se va a mostrar la lista de pedidos");

        return View("Index", await _pedidoRepository.AllAsync());
    }

    // GET: Pedidos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        _logger.LogInfo($"Se va a mostrar los detalles del pedido con id = {id}");

        if (id == null) return NotFound();

        var pedido = await _pedidoRepository.GetByIdAsync((int)id);

        if (pedido == null) return NotFound();

        return View("Details", pedido);
    }

    // GET: Pedidos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Pedidos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PedidoDto pedido)
    {
        _logger.LogInfo("Se va a crear un pedido");

        if (!ModelState.IsValid) return View(pedido);

        await _pedidoRepository.AddAsync(pedido);

        return RedirectToAction(nameof(Index));
    }

    // GET: Pedidos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para editar el pedido con id = {id}");

        if (id == null) return NotFound();

        var pedido = await _pedidoRepository.GetByIdAsync((int)id);

        if (pedido == null) return NotFound();

        return View(pedido);
    }

    // POST: Pedidos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PedidoDto pedido)
    {
        _logger.LogInfo($"Se va a editar el pedido con id = {id}");

        if (id != pedido.Id) return NotFound();

        if (!ModelState.IsValid) return View(pedido);

        try
        {
            await _pedidoRepository.UpdateAsync(pedido);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await PedidoExists(pedido.Id)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Pedidos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        _logger.LogInfo($"Se va a mostrar la vista para eliminar el pedido con id = {id}");

        if (id == null) return NotFound();

        var pedido = await _pedidoRepository.GetByIdAsync((int)id);

        if (pedido == null) return NotFound();

        return View(pedido);
    }

    // POST: Pedidos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _logger.LogInfo($"Se va a eliminar el pedido con id = {id}");

        await _pedidoRepository.DeleteAsync((int)id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> PedidoExists(int id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        return pedido != null;
    }
}
