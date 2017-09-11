using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace LrocreShop.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetByAlias(string alias);
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
    }
}