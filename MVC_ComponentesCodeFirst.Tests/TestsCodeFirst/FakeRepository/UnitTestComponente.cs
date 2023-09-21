using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Services.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.FakeRepository
{
    [TestClass]
    public class UnitTestComponente
    {
        private FakeComponenteRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new FakeComponenteRepository();
        }

        [TestMethod]
        public async Task AllAsync_ReturnsAllComponentes()
        {

            var componentes = await _repository.AllAsync();


            Assert.IsNotNull(componentes);
            Assert.AreEqual(15, componentes.Count);

        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsComponenteById()
        {

            var componente = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(componente);
            Assert.AreEqual(1, componente.Id);
            Assert.AreEqual("Procesador Intel i7", componente.Descripcion);

        }

        [TestMethod]
        public async Task AddAsync_AddsNewComponente()
        {

            var nuevoComponente = new ComponenteDto
            {
                Descripcion = "Nuevo Componente",
                Calor = 20,
                Cores = 4,
                Coste = 75,
                Megas = 1024,
                NumeroDeSerie = "123-XYZ",
                Tipo = TipoComponente.Procesador
            };


            await _repository.AddAsync(nuevoComponente);
            var componentes = await _repository.AllAsync();


            Assert.IsNotNull(componentes);
            Assert.AreEqual(16, componentes.Count);
            var componenteAgregado = componentes.FirstOrDefault(c => c.Descripcion == "Nuevo Componente");
            Assert.IsNotNull(componenteAgregado);
            Assert.AreEqual("123-XYZ", componenteAgregado.NumeroDeSerie);

        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesComponente()
        {

            var componenteActualizado = new ComponenteDto
            {
                Id = 1,
                Descripcion = "Procesador Intel i7 Actualizado",
                Calor = 12,
                Cores = 8,
                Coste = 150,
                Megas = 2048,
                NumeroDeSerie = "789-XYZ",
                Tipo = TipoComponente.Procesador
            };


            await _repository.UpdateAsync(componenteActualizado);
            var componente = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(componente);
            Assert.AreEqual("Procesador Intel i7 Actualizado", componente.Descripcion);
            Assert.AreEqual(12, componente.Calor);
            Assert.AreEqual(8, componente.Cores);
            Assert.AreEqual(150.0, componente.Coste);

        }

        [TestMethod]
        public async Task DeleteAsync_DeletesComponente()
        {

            await _repository.DeleteAsync(2);
            var componente = await _repository.GetByIdAsync(2);


            Assert.IsNull(componente);
        }
    }
}
