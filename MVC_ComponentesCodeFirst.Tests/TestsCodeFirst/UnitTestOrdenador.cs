using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Pedidos;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst;

[TestClass]
public class UnitTestOrdenador
{
    private readonly OrdenadoresController _controlador = new(new FakeOrdenadorRepository(), new FakePedidoRepository(), new LoggerManager());

    [TestMethod]
    public async Task TestOrdenadorIndexView()
    {
        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenadores = result.ViewData.Model as List<OrdenadorDto>;
        Assert.IsNotNull(ordenadores);
        Assert.AreEqual(1, ordenadores.Count);
    }

    [TestMethod]
    public async Task TestOrdenadorDetailsView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenador = result.ViewData.Model as OrdenadorDto;
        Assert.IsNotNull(ordenador);
        Assert.AreEqual(1, ordenador.Id);
        Assert.AreEqual("OrdenadorPrueba", ordenador.Descripcion);
    }

    [TestMethod]
    public async Task TestOrdenadorCreateView()
    {
        OrdenadorDto nuevoOrdenador = new()
        {
            Id = 0,
            Descripcion = "OrdenadorNuevo",
        };

        await _controlador.Create(nuevoOrdenador);

        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenadores = result.ViewData.Model as List<OrdenadorDto>;
        Assert.IsNotNull(ordenadores);
        Assert.AreEqual(2, ordenadores.Count);


        var ordenadorCreado = ordenadores.Find(ordenador => ordenador.Id == 2);
        Assert.IsNotNull(ordenadorCreado);
        Assert.AreEqual(2, ordenadorCreado.Id);
        Assert.AreEqual("OrdenadorNuevo", ordenadorCreado.Descripcion);
    }

    [TestMethod]
    public async Task TestOrdenadorEditView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenador = result.ViewData.Model as OrdenadorDto;
        Assert.IsNotNull(ordenador);
        Assert.AreEqual("OrdenadorPrueba", ordenador.Descripcion);

        ordenador.Descripcion = "Nueva descripcion del OrdenadorPrueba";
        await _controlador.Edit(1, ordenador);

        result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenadorEditado = result.ViewData.Model as OrdenadorDto;
        Assert.IsNotNull(ordenadorEditado);
        Assert.AreEqual("Nueva descripcion del OrdenadorPrueba", ordenadorEditado.Descripcion);
    }

    [TestMethod]
    public async Task TestOrdenadorDeleteView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var ordenadorAEliminar = result.ViewData.Model as OrdenadorDto;
        Assert.IsNotNull(ordenadorAEliminar);

        await _controlador.DeleteConfirmed(1);

        var notFoundResult = await _controlador.Details(1) as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
