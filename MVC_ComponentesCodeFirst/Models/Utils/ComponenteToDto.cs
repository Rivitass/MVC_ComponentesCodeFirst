using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Models.Utils;

public static class ComponenteToDto
{
    public static ComponenteDto Convert(Componente componente)
    {
        return new ComponenteDto()
        {
            Id = componente.Id,
            NumeroDeSerie = componente.NumeroDeSerie,
            Descripcion = componente.Descripcion,
            Calor = componente.Calor,
            Megas = componente.Megas,
            Cores = componente.Cores,
            Coste = componente.Coste,
            Tipo = componente.Tipo,
            OrdenadorId = componente.OrdenadorId,
            Ordenador = componente.Ordenador == null ? null : OrdenadorToDto.ConvertWithoutIncludeComponentes(componente.Ordenador)
        };
    }

    public static ComponenteDto ConvertWithoutIncludeOrdenador(Componente componente)
    {
        return new ComponenteDto()
        {
            Id = componente.Id,
            NumeroDeSerie = componente.NumeroDeSerie,
            Descripcion = componente.Descripcion,
            Calor = componente.Calor,
            Megas = componente.Megas,
            Cores = componente.Cores,
            Coste = componente.Coste,
            Tipo = componente.Tipo,
            OrdenadorId = componente.OrdenadorId
        };
    }
}
