using System.ComponentModel.DataAnnotations;
using MVC_ComponentesCodeFirst.Models.Ordenadores;

namespace MVC_ComponentesCodeFirst.Models.Pedidos;

public class Pedido
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime Fecha { get; set; }

    public ICollection<Ordenador> Ordenadores { get; set; } = new List<Ordenador>();
}
