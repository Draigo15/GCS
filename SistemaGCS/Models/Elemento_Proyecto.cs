using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGCS.Models
{
    [Table("Elemento_Proyecto")]
    public class Elemento_Proyecto
    {
        [Key]
        public int Id_elemento_proyecto { get; set; }

        [Required]
        public int Id_proyecto { get; set; }

        [Required]
        public int Id_elementoconfiguracion { get; set; }

        [StringLength(1)]
        public string Estado { get; set; } = "A";

        // Relaciones de navegación
        public virtual Proyecto Proyecto { get; set; }

        public virtual Elemento_Configuracion Elemento_Configuracion { get; set; }
    }
}
