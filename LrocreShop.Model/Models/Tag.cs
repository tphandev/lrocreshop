using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LrocreShop.Model.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [Column(TypeName ="varchar")]
        [MaxLength(50)]
        public string ID { get; set; }

      
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

     
        [MaxLength(50)]
        [Required]
        public string type { get; set; }
    }
}