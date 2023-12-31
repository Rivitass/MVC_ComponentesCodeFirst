﻿using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Utils;

namespace MVC_ComponentesCodeFirst.Services.Componentes;

public class FakeComponenteRepository : IComponenteRepository
{
    private readonly List<Componente> _componentes = new()
    {
        new Componente { Id = 1, Calor = 10, Cores = 9, Coste = 134, Descripcion = "Procesador Intel i7", Megas = 0, NumeroDeSerie = "789-XCS", Tipo = TipoComponente.Procesador },
        new Componente { Id = 2, Calor = 12, Cores = 10, Coste = 138, Descripcion = "Procesador Intel i7", Megas = 0, NumeroDeSerie = "789-XCD", Tipo = TipoComponente.Procesador },
        new Componente { Id = 3, Calor = 22, Cores = 11, Coste = 138, Descripcion = "Procesador Intel i7", Megas = 0, NumeroDeSerie = "789-XCT", Tipo = TipoComponente.Procesador },
        new Componente { Id = 4, Calor = 10, Cores = 0, Coste = 100, Descripcion = "Banco de Memoria SDRAM", Megas = 512, NumeroDeSerie = "879FH", Tipo = TipoComponente.RAM },
        new Componente { Id = 5, Calor = 15, Cores = 0, Coste = 125, Descripcion = "Banco de Memoria SDRAM", Megas = 1024, NumeroDeSerie = "879FH-L", Tipo = TipoComponente.RAM },
        new Componente { Id = 6, Calor = 24, Cores = 0, Coste = 150, Descripcion = "Banco de Memoria SDRAM", Megas = 1024, NumeroDeSerie = "879FH-T", Tipo = TipoComponente.RAM },
        new Componente { Id = 7, Calor = 10, Cores = 0, Coste = 50, Descripcion = "DiscoDuro SanDisk", Megas = 1024 * 500, NumeroDeSerie = "789-XX", Tipo = TipoComponente.DiscoDuro },
        new Componente { Id = 8, Calor = 29, Cores = 0, Coste = 90, Descripcion = "DiscoDuro SanDisk", Megas = 1024 * 1000, NumeroDeSerie = "789-XX-2", Tipo = TipoComponente.DiscoDuro },
        new Componente { Id = 9, Calor = 39, Cores = 0, Coste = 128, Descripcion = "DiscoDuro SanDisk", Megas = 1024 * 2000, NumeroDeSerie = "789-XX-3", Tipo = TipoComponente.DiscoDuro },
        new Componente { Id = 10, Calor = 30, Cores = 10, Coste = 78, Descripcion = "Procesador Ryzen AMD", Megas = 0, NumeroDeSerie = "797-X", Tipo = TipoComponente.Procesador },
        new Componente { Id = 11, Calor = 30, Cores = 29, Coste = 178, Descripcion = "Procesador Ryzen AMD", Megas = 0, NumeroDeSerie = "797-X2", Tipo = TipoComponente.Procesador },
        new Componente { Id = 12, Calor = 60, Cores = 34, Coste = 278, Descripcion = "Procesador Ryzen AMD", Megas = 0, NumeroDeSerie = "797-X3", Tipo = TipoComponente.Procesador },
        new Componente { Id = 13, Calor = 35, Cores = 0, Coste = 37, Descripcion = "Disco Mecánico Patatin", Megas = 250, NumeroDeSerie = "788-fg", Tipo = TipoComponente.DiscoDuro },
        new Componente { Id = 14, Calor = 35, Cores = 0, Coste = 67, Descripcion = "Disco Mecánico Patatin", Megas = 250, NumeroDeSerie = "788-fg-2", Tipo = TipoComponente.DiscoDuro },
        new Componente { Id = 15, Calor = 35, Cores = 0, Coste = 97, Descripcion = "Disco Mecánico Patatin", Megas = 250, NumeroDeSerie = "788-fg-3", Tipo = TipoComponente.DiscoDuro }
    };

    public Task<List<ComponenteDto>> AllAsync()
    {
        return Task.FromResult(_componentes.Select(ComponenteToDto.Convert).ToList());
    }

    public Task<ComponenteDto?> GetByIdAsync(int id)
    {
        return Task.FromResult(_componentes.Select(ComponenteToDto.Convert).FirstOrDefault(componente => componente.Id == id));
    }

    public Task AddAsync(ComponenteDto componente)
    {
        int nuevoId = _componentes.Count + 1;
        componente.Id = nuevoId;

        _componentes.Add(new Componente()
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
        });

        return Task.CompletedTask;
    }

    public Task UpdateAsync(ComponenteDto componente)
    {
        var componenteActualizado = _componentes.Find(element => element.Id.Equals(componente.Id));

        if (componenteActualizado is null) return Task.CompletedTask;

        componenteActualizado.Id = componente.Id;
        componenteActualizado.NumeroDeSerie = componente.NumeroDeSerie;
        componenteActualizado.Descripcion = componente.Descripcion;
        componenteActualizado.Calor = componente.Calor;
        componenteActualizado.Megas = componente.Megas;
        componenteActualizado.Cores = componente.Cores;
        componenteActualizado.Coste = componente.Coste;
        componenteActualizado.Tipo = componente.Tipo;
        componenteActualizado.OrdenadorId = componente.OrdenadorId;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Componente? componente = _componentes.Find(componentes => componentes.Id == id);

        if (componente != null)
        {
            _componentes.Remove(componente);
        }

        return Task.CompletedTask;
    }
}
