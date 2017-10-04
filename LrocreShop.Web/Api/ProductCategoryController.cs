using AutoMapper;
using LrocreShop.Model.Models;
using LrocreShop.Service;
using LrocreShop.Web.Infrastructure.Core;
using LrocreShop.Web.Infrastructure.Extensions;
using LrocreShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LrocreShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request,string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProductCategory = _productCategoryService.GetAll(keyword);

                totalRow = listProductCategory.Count();

                var query = listProductCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listPostCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listPostCategoryVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("getallparents")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProductCategory = _productCategoryService.GetAll();

                var listPostCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);             

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }

        [Route("create")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    ProductCategory newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);

                    var productCategory = _productCategoryService.Add(newProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }
    }
}