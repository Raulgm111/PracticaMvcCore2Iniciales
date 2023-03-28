using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("V_GRUPO_LIBROS")]
    public class VistaLibros
    {
        [Key]
        [Column("IDLIBRO")]
        public int Idlibro { get; set; }
        [Column("TITULO")]
        public string Titulo { get; set; }
        [Column("AUTOR")]
        public string Autor { get; set; }
        [Column("POSICION")]
        public int Posicion { get; set; }
    }
}
