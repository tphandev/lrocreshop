using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LrocreShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);
        IEnumerable<Product> GetListByTag(string tagId, int page, int pagesize, out int totalRow);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            return this.DbContext.Products.Where(x => x.Alias == alias);
        }

        public IEnumerable<Product> GetListByTag(string tagId, int page, int pagesize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;
            totalRow = query.Count();
            return query.OrderByDescending(x=>x.CreatedDate).Skip((page - 1) * pagesize).Take(pagesize);
        }
    }
}