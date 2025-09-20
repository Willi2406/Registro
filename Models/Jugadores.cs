using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage ="Las partidas son obligatorias")]
        [Range(0, int.MaxValue, ErrorMessage = "Las partidas deben ser >= 0")]

        public int Victorias { get; set; }

        public int Derrotas { get; set; }

        public int Empate { get; set; }

        [InverseProperty(nameof(Models.Movimientos.Jugadores))]
        public virtual ICollection<Movimientos> Movimientos { get; set; } = new List<Movimientos>();
    }
}
