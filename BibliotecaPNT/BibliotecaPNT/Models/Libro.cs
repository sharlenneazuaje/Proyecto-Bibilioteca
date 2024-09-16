using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BibliotecaPNT.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)] 
        public string autor { get; set; }

        [Required]
        [MaxLength(50)]
        public string editorial { get; set; }

        [Required]
        public int año { get; set; }

        [Required]
        [MaxLength(50)]
        public string titulo { get; set; }

        [Required]
        [EnumDataType(typeof(Genero), ErrorMessage = "El género ingresado no es válido.")]
        public Genero genero { get; set; }

        public bool prestado { get; set; }

        public string? prestatario { get; set; }
    }
}
