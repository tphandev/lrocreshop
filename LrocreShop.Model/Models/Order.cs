using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LrocreShop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(250)]
        public string CustomerEmail { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [Required]
        [MaxLength(250)]
        public string CustomerMessage { get; set; }

        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { set; get; }
        public bool Status { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}