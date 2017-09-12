using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;

namespace LrocreShop.Data.Repositories
{
    public interface IFooterRepository:IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}