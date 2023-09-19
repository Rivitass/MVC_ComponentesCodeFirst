using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Services.Componentes;
using MVC_ComponentesCodeFirst.Services.Ordenadores;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst;

[TestClass]
public class UnitTestComponente
{
    private readonly ComponentesController _controlador = new(new FakeComponenteRepository(), new FakeOrdenadorRepository(), new LoggerManager());

    [TestMethod]
    public async Task TestComponenteIndexView()
    {
        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var componentes = result.ViewData.Model as List<ComponenteDto>;
        Assert.IsNotNull(componentes);
        Assert.AreEqual(15, componentes.Count);
    }

    [TestMethod]
    public async Task TestComponenteDetailsView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ViewName);
        Assert.IsNotNull(result.ViewData.Model);

        var componente = result.ViewData.Model as ComponenteDto;
        Assert.IsNotNull(componente);
        Assert.AreEqual(1, componente.Id);
        Assert.AreEqual(10, componente.Calor);
        Assert.AreEqual(9, componente.Cores);
        Assert.AreEqual(134, componente.Coste);
        Assert.AreEqual("Procesador Intel i7", componente.Descripcion);
        Assert.AreEqual(0, componente.Megas);
        Assert.AreEqual("789-XCS", componente.NumeroDeSerie);
        Assert.AreEqual(TipoComponente.Procesador, componente.Tipo);
    }

    [TestMethod]
    public async Task TestComponenteCreateView()
    {
        ComponenteDto nuevoComponente = new()
        {
            Id = 0,
            Calor = 10,
            Cores = 0,
            Coste = 134,
            Descripcion = "Disco Externo Sam",
            Megas = 1024 * 9000,
            NumeroDeSerie = "1789-XCS",
            Tipo = TipoComponente.DiscoDuro
        };

        await _controlador.Create(nuevoComponente);

        var result = await _controlador.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var componentes = result.ViewData.Model as List<ComponenteDto>;
        Assert.IsNotNull(componentes);
        Assert.AreEqual(16, componentes.Count);


        var componenteCreado = componentes.Find(componente => componente.Id == 16);
        Assert.IsNotNull(componenteCreado);
        Assert.AreEqual(16, componenteCreado.Id);
        Assert.AreEqual(10, componenteCreado.Calor);
        Assert.AreEqual(0, componenteCreado.Cores);
        Assert.AreEqual(134, componenteCreado.Coste);
        Assert.AreEqual("Disco Externo Sam", componenteCreado.Descripcion);
        Assert.AreEqual(1024 * 9000, componenteCreado.Megas);
        Assert.AreEqual("1789-XCS", componenteCreado.NumeroDeSerie);
        Assert.AreEqual(TipoComponente.DiscoDuro, componenteCreado.Tipo);
    }

    [TestMethod]
    public async Task TestComponenteEditView()
    {
        var result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var componente = result.ViewData.Model as ComponenteDto;
        Assert.IsNotNull(componente);
        Assert.AreEqual("Procesador Intel i7", componente.Descripcion);

        componente.Descripcion = "Nueva descripcion del procesador Intel i7";
        await _controlador.Edit(1, componente);

        result = await _controlador.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var componenteEditado = result.ViewData.Model as ComponenteDto;
        Assert.IsNotNull(componenteEditado);
        Assert.AreEqual("Nueva descripcion del procesador Intel i7", componenteEditado.Descripcion);
    }

    [TestMethod]
    public async Task TestComponenteDeleteView()
    {
        var result = await _controlador.Details(2) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var componenteAEliminar = result.ViewData.Model as ComponenteDto;
        Assert.IsNotNull(componenteAEliminar);

        await _controlador.DeleteConfirmed(2);

        var notFoundResult = await _controlador.Details(2) as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
