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
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        #region Initialize
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }
        #endregion
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProduct = _productService.GetAll(keyword);

                totalRow = listProduct.Count();

                var query = listProduct.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listProductVm = Mapper.Map<List<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = listProductVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var product = _productService.GetByID(id);

                var productVm = Mapper.Map<ProductViewModel>(product);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productVm);

                return response;
            });
        }

        [Route("create")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Product newProduct = new Product();
                    newProduct.UpdateProduct(productVm);
                    newProduct.CreatedDate = DateTime.Now;
                    newProduct.CreatedBy = User.Identity.Name;
                    var product = _productService.Add(newProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<ProductViewModel>(product);
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
        public HttpResponseMessage Put(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Product dbProduct = _productService.GetByID(productVm.ID);
                    dbProduct.UpdateProduct(productVm);
                    dbProduct.UpdatedDate = DateTime.Now;

                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<ProductViewModel>(dbProduct);
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
                    var oldProduct = _productService.Delete(id);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<ProductViewModel>(oldProduct);
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
        public HttpResponseMessage Delete(HttpRequestMessage request, string checkedProductIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProductIds = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductIds);
                    foreach (var item in listProductIds)
                    {
                        _productService.Delete(item);

                    }

                    _productService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductIds.Count());
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