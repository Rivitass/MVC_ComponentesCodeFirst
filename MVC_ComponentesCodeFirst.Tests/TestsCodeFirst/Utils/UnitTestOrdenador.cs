using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using MVC_ComponentesCodeFirst.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.Utils
{
    [TestClass]
    public class UnitTestOrdenador
    {
        [TestMethod]
        public void Convert_ConvertsOrdenadorToDto()
        {

            var ordenador = new Ordenador
            {
                Id = 1,
                Descripcion = "Ordenador de prueba",
                PedidoId = 101,
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
                },
                Pedido = new Pedido
                {

                }
            };


            var ordenadorDto = OrdenadorToDto.Convert(ordenador);


            Assert.AreEqual(1, ordenadorDto.Id);
            Assert.AreEqual("Ordenador de prueba", ordenadorDto.Descripcion);
            Assert.AreEqual(101, ordenadorDto.PedidoId);
            Assert.AreEqual(125.0, ordenadorDto.Coste);

        }

        [TestMethod]
        public void ConvertsOrdenadorToDtoWithoutComponentes()
        {

            var ordenador = new Ordenador
            {
                Id = 2,
                Descripcion = "Otro ordenador de prueba",
                PedidoId = 202,
                Pedido = new Pedido
                {

                }
            };


            var ordenadorDto = OrdenadorToDto.ConvertWithoutIncludeComponentes(ordenador);


            Assert.AreEqual(2, ordenadorDto.Id);
            Assert.AreEqual("Otro ordenador de prueba", ordenadorDto.Descripcion);
            Assert.AreEqual(202, ordenadorDto.PedidoId);
            Assert.AreEqual(0.0, ordenadorDto.Coste);

        }
    }
}
