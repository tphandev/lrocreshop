using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LrocreShop.Model.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [MaxLength(50)]
        public string ID { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        [Key]
        [MaxLength(50)]
        public string type { get; set; }
    }
}