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
using System.Web.Script.Serialization;

namespace LrocreShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        #endregion
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProductCategory = _productCategoryService.GetAll(keyword);

                totalRow = listProductCategory.Count();

                var query = listProductCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listProductCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategoryVm,
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

                var listProductCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request,int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var productCategory = _productCategoryService.GetByID(id);

                var productCategoryVm = Mapper.Map<ProductCategoryViewModel>(productCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productCategoryVm);

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
                    newProductCategory.CreatedDate = DateTime.Now;
                    newProductCategory.CreatedBy = User.Identity.Name;
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

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm) 
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    ProductCategory dbProductCategory =_productCategoryService.GetByID(productCategoryVm.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    dbProductCategory.CreatedBy = User.Identity.Name;
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategoryViewModel>(dbProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldProductCategory= _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategoryViewModel>(oldProductCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string checkedProductCategoryIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProductCategoryIds = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategoryIds);
                    foreach (var item in listProductCategoryIds)
                    {
                        _productCategoryService.Delete(item);
                     
                    }

                    _productCategoryService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryIds.Count());
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