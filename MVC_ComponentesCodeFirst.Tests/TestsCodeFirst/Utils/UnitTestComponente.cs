using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_ComponentesCodeFirst.Tests.TestsCodeFirst.Utils
{
    [TestClass]  
    public class UnitTestComponente
    {
        [TestMethod]
        public void Convert_ConvertsComponenteToDto()
        {

            var componente = new Componente
            {
                Id = 1,
                NumeroDeSerie = "123-ABC",
                Descripcion = "Componente de prueba",
                Calor = 42,
                Megas = 2048,
                Cores = 4,
                Coste = 99,
                Tipo = TipoComponente.Procesador,
                OrdenadorId = 100,
                Ordenador = new Ordenador
                {
                   
                }
            };

         
            var componenteDto = ComponenteToDto.Convert(componente);

     
            Assert.AreEqual(1, componenteDto.Id);
            Assert.AreEqual("123-ABC", componenteDto.NumeroDeSerie);

        }

        [TestMethod]
        public void ConvertsComponenteToDtoWithoutOrdenador()
        {
            
            var componente = new Componente
            {
                Id = 2,
                NumeroDeSerie = "456-XYZ",
                Descripcion = "Otro componente de prueba",
                Calor = 30,
                Megas = 1024,
                Cores = 2,
                Coste = 49,
                Tipo = TipoComponente.RAM,
                OrdenadorId = 200
            };

           
            var componenteDto = ComponenteToDto.ConvertWithoutIncludeOrdenador(componente);

           
            Assert.AreEqual(2, componenteDto.Id);
            Assert.AreEqual("456-XYZ", componenteDto.NumeroDeSerie);
         
        }
    }
}
