using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.Utils
{
    [TestClass]
    public class UnitTestPedido
    {
        [TestMethod]
        public void Convert_ConvertsPedidoToDto()
        {

            var pedido = new Pedido
            {
                Id = 1,
                Descripcion = "Pedido de prueba",
                Fecha = DateTime.Now,
                Ordenadores = new List<Ordenador>
                {
                    new Ordenador
                    {
                        Id = 101,
                        Descripcion = "Ordenador 1",
                        Componentes = new List<Componente>
                        {
                            new Componente
                            {
                                Id = 11,
                                Coste = 50
                            },
                            new Componente
                            {
                                Id = 12,
                                Coste = 75
                            }
                        }
                    },
                    new Ordenador
                    {
                        Id = 102,
                        Descripcion = "Ordenador 2",
                        Componentes = new List<Componente>
                        {
                            new Componente
                            {
                                Id = 13,
                                Coste = 60
                            },
                            new Componente
                            {
                                Id = 14,
                                Coste = 90
                            }
                        }
                    }
                }
            };


            var pedidoDto = PedidoToDto.Convert(pedido);


            Assert.AreEqual(1, pedidoDto.Id);
            Assert.AreEqual("Pedido de prueba", pedidoDto.Descripcion);
            Assert.IsNotNull(pedidoDto.Fecha);
            Assert.AreEqual(275.0, pedidoDto.Coste);
            Assert.IsNotNull(pedidoDto.Ordenadores);
            Assert.AreEqual(2, pedidoDto.Ordenadores.Count);

        }

        //[TestMethod]
        //public void ConvertsPedidoToDtoWithoutOrdenadores()
        //{

        //    var pedido = new Pedido
        //    {
        //        Id = 2,
        //        Descripcion = "Otro pedido de prueba",
        //        Fecha = DateTime.Now
        //    };

        //    var pedidoDto = PedidoToDto.ConvertWithoutIncludeOrdenadores(pedido);


        //    Assert.AreEqual(2, pedidoDto.Id);
        //    Assert.AreEqual("Otro pedido de prueba", pedidoDto.Descripcion);
        //    Assert.IsNotNull(pedidoDto.Fecha);
        //    Assert.AreEqual(0.0, pedidoDto.Coste);
        //    Assert.IsNull(pedidoDto.Ordenadores);

        //}
    }
}
