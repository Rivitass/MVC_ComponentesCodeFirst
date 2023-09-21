using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Ordenadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.FakeRepository
{
    [TestClass]
    public class UnitTestOrdenador
    {
        private FakeOrdenadorRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new FakeOrdenadorRepository();
        }

        [TestMethod]
        public async Task AllAsync_ReturnsAllOrdenadores()
        {

            var ordenadores = await _repository.AllAsync();


            Assert.IsNotNull(ordenadores);
            Assert.AreEqual(1, ordenadores.Count);

        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsOrdenadorById()
        {

            var ordenador = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(ordenador);
            Assert.AreEqual(1, ordenador.Id);
            Assert.AreEqual("OrdenadorPrueba", ordenador.Descripcion);

        }

        [TestMethod]
        public async Task AddAsync_AddsNewOrdenador()
        {

            var nuevoOrdenador = new OrdenadorDto
            {
                Descripcion = "Nuevo Ordenador",
                PedidoId = 101
            };

            await _repository.AddAsync(nuevoOrdenador);
            var ordenadores = await _repository.AllAsync();


            Assert.IsNotNull(ordenadores);
            Assert.AreEqual(2, ordenadores.Count);
            var ordenadorAgregado = ordenadores.FirstOrDefault(o => o.Descripcion == "Nuevo Ordenador");
            Assert.IsNotNull(ordenadorAgregado);
            Assert.AreEqual(101, ordenadorAgregado.PedidoId);

        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesOrdenador()
        {

            var ordenadorActualizado = new OrdenadorDto
            {
                Id = 1,
                Descripcion = "OrdenadorPruebaActualizado",
                PedidoId = 102
            };


            await _repository.UpdateAsync(ordenadorActualizado);
            var ordenador = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(ordenador);
            Assert.AreEqual("OrdenadorPruebaActualizado", ordenador.Descripcion);
            Assert.AreEqual(102, ordenador.PedidoId);

        }

        [TestMethod]
        public async Task DeleteAsync_DeletesOrdenador()
        {

            await _repository.DeleteAsync(1);
            var ordenador = await _repository.GetByIdAsync(1);


            Assert.IsNull(ordenador);
        }
    }
}
