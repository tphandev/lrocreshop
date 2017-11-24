using AutoMapper;
using LrocreShop.Service;
using LrocreShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LrocreShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;
        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _commonService = commonService;
        }
        [OutputCache(Duration = 60,Location =System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var sildeViewModel = Mapper.Map<IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
           

            var lastestProductModel = _productService.GetLastest(3);
            var topSaleProductModel = _productService.GetHot(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(topSaleProductModel);

            homeViewModel.Slides = sildeViewModel;
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration=3600)]
        public ActionResult Footer()
        {
            var footerModel=_commonService.GetFooter();
            var footerViewModel = Mapper.Map<FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll().OrderBy(x=>x.DisplayOrder);
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}