﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LrocreShop.Web.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }
        
        public string Name { set; get; }
        public string Alias { set; get; }

        public int CategoryID { set; get; }
        
        public string Image { get; set; }
        
        public string MoreImages { set; get; }
        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }
        public int? Warranty { set; get; }


        public string Description { set; get; }
        public string Content { get; set; }

        public bool? HotFlag { set; get; }
        public bool? HomeFlag { set; get; }
        public int? ViewCount { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        public bool Status { set; get; }

        public string Tags { get; set; }

        public int Quantity { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { set; get; }

        public virtual IEnumerable<ProductTagViewModel> ProductTags { set; get; }
    }
}