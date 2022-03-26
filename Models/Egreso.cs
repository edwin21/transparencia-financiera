using System.ComponentModel.DataAnnotations;

namespace TransparenciaFinanciera.Models
{
  public class Egreso
  {
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }
  }
}