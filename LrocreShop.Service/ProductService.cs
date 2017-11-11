using LrocreShop.Common;
using LrocreShop.Data.Infrastructure;
using LrocreShop.Data.Repositories;
using LrocreShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LrocreShop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllByCategoryId(int categoryId);

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHot(int top);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetReatedProducts(int id, int top);

        IEnumerable<string> GetListProductByName(string keyword);

        Product GetByID(int id);

        IEnumerable<Tag> GetListTagByProdictId(int id);

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);

        Tag GetTag(string tagId);

        void IncreaseView(int id);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var newProduct = _productRepository.Add(product);
            //_unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagName = tags[i].Trim();
                    var tagId = StringHelper.ToUnsignString(tagName);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tagName;
                        tag.type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return newProduct;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllByCategoryId(int categoryId)
        {
            return _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
        }

        public Product GetByID(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHot(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            totalRow = query.Count();
            switch (sort)
            {

                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string keyword)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword)).Select(x => x.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            totalRow = query.Count();
            switch (sort)
            {

                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByProdictId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(x => x.Tag);
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {

            return _productRepository.GetListByTag(tagId, page, pageSize, out totalRow);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }
    }
}