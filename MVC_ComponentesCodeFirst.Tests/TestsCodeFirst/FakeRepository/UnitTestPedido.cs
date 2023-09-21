using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Services.Pedidos;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.FakeRepository
{
    [TestClass]
    public class UnitTestPedido
    {
        private FakePedidoRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new FakePedidoRepository();
        }

        [TestMethod]
        public async Task AllAsync_ReturnsAllPedidos()
        {

            var pedidos = await _repository.AllAsync();


            Assert.IsNotNull(pedidos);
            Assert.AreEqual(1, pedidos.Count);

        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsPedidoById()
        {

            var pedido = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(pedido);
            Assert.AreEqual(1, pedido.Id);
            Assert.AreEqual("Pedido de prueba 1", pedido.Descripcion);

        }

        [TestMethod]
        public async Task AddAsync_AddsNewPedido()
        {

            var nuevoPedido = new PedidoDto
            {
                Descripcion = "Nuevo Pedido",
                Fecha = new DateTime(2023, 9, 1),
                Ordenadores = new List<OrdenadorDto>()
            };


            await _repository.AddAsync(nuevoPedido);
            var pedidos = await _repository.AllAsync();


            Assert.IsNotNull(pedidos);
            Assert.AreEqual(2, pedidos.Count);
            var pedidoAgregado = pedidos.FirstOrDefault(p => p.Descripcion == "Nuevo Pedido");
            Assert.IsNotNull(pedidoAgregado);
            Assert.AreEqual(new DateTime(2023, 9, 1), pedidoAgregado.Fecha);

        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesPedido()
        {

            var pedidoActualizado = new PedidoDto
            {
                Id = 1,
                Descripcion = "Pedido de prueba 1 Actualizado",
                Fecha = new DateTime(2023, 9, 2),
                Ordenadores = new List<OrdenadorDto>()
            };


            await _repository.UpdateAsync(pedidoActualizado);
            var pedido = await _repository.GetByIdAsync(1);


            Assert.IsNotNull(pedido);
            Assert.AreEqual("Pedido de prueba 1 Actualizado", pedido.Descripcion);
            Assert.AreEqual(new DateTime(2023, 9, 2), pedido.Fecha);

        }

        [TestMethod]
        public async Task DeleteAsync_DeletesPedido()
        {

            await _repository.DeleteAsync(1);
            var pedido = await _repository.GetByIdAsync(1);


            Assert.IsNull(pedido);
        }
    }
}
