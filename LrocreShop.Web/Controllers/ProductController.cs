using AutoMapper;
using LrocreShop.Common;
using LrocreShop.Service;
using LrocreShop.Web.Infrastructure.Core;
using LrocreShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LrocreShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetByID(id);
            var productViewModel = Mapper.Map<ProductViewModel>(productModel);
            var relatedProducts = _productService.GetReatedProducts(id, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<ProductViewModel>>(relatedProducts);
            var moreImages = productViewModel.MoreImages;
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(moreImages);
            ViewBag.MoreImages = listImages;
            return View(productViewModel);
        }
        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var ProductViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var category = _productCategoryService.GetByID(id);
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = ProductViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var ProductViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = ProductViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var name = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = name
            }, JsonRequestBehavior.AllowGet);
        }
    }
}