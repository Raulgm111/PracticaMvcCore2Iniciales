using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("APELLIDOS")]
        public string Aepllidos { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("PASS")]
        public string Password { get; set; }
        [Column("FOTO")]
        public string Imagen { get; set; }
    }
}
