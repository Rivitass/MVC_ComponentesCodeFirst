using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Services.Pedidos;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst;

[TestClass]
public class UnitTestPedido
{
    private readonly PedidosController _controlador = new(new FakePedidoRepository(), new LoggerManager());

    [TestMethod]
    public async Task TestPedidoIndexView()
    {
        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var pedidos = result.ViewData.Model as List<PedidoDto>;
        Assert.IsNotNull(pedidos);
        Assert.AreEqual(1, pedidos.Count);
    }

    [TestMethod]
    public async Task TestPedidoDetailsView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var pedido = result.ViewData.Model as PedidoDto;
        Assert.IsNotNull(pedido);
        Assert.AreEqual(1, pedido.Id);
        Assert.AreEqual("Pedido de prueba 1", pedido.Descripcion);
    }

    [TestMethod]
    public async Task TestPedidoCreateView()
    {
        PedidoDto nuevoPedido = new()
        {
            Id = 0,
            Descripcion = "PedidoNuevo"
        };

        await _controlador.Create(nuevoPedido);

        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var pedidos = result.ViewData.Model as List<PedidoDto>;
        Assert.IsNotNull(pedidos);
        Assert.AreEqual(2, pedidos.Count);

        var pedidoCreado = pedidos.Find(pedido => pedido.Id == 2);
        Assert.IsNotNull(pedidoCreado);
        Assert.AreEqual(2, pedidoCreado.Id);
        Assert.AreEqual("PedidoNuevo", pedidoCreado.Descripcion);
    }

    [TestMethod]
    public async Task TestPedidoEditView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var pedido = result.ViewData.Model as PedidoDto;
        Assert.IsNotNull(pedido);
        Assert.AreEqual("Pedido de prueba 1", pedido.Descripcion);

        pedido.Descripcion = "Nueva descripcion del Pedido de prueba 1";
        await _controlador.Edit(1, pedido);

        result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var pedidoEditado = result.ViewData.Model as PedidoDto;
        Assert.IsNotNull(pedidoEditado);
        Assert.AreEqual("Nueva descripcion del Pedido de prueba 1", pedidoEditado.Descripcion);
    }

    [TestMethod]
    public async Task TestPedidoDeleteView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var pedidoAEliminar = result.ViewData.Model as PedidoDto;
        Assert.IsNotNull(pedidoAEliminar);

        await _controlador.DeleteConfirmed(1);

        var notFoundResult = await _controlador.Details(1) as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
