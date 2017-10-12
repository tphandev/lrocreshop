using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LrocreShop.Web.Models
{
    public class ProductCategoryViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Yêu cầu nhập tên danh mục")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tiêu đề SEO")]
        public string Alias { set; get; }

    
        public string Description { set; get; }
        public int? ParentID { get; set; }
        public int? DisplayOrder { get; set; }

      
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}