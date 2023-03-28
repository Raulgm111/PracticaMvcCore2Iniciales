using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        [Column("IDGENERO")]
        public int IdGenero { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}
