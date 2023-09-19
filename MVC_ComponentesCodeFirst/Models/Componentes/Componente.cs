using System.ComponentModel.DataAnnotations;
using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Models.Componentes;

public class Componente
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 1)]
    public string NumeroDeSerie { get; set; }

    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Calor { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public long Megas { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Cores { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Coste { get; set; }

    [Required]
    public TipoComponente Tipo { get; set; }

    // Foreign Keys
    public int? OrdenadorId { get; set; } // Optional foreign key property

    public Ordenador? Ordenador { get; set; } // Optional reference navigation to principal
}
