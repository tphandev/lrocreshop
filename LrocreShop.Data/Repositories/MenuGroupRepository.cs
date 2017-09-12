using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;

namespace LrocreShop.Data.Repositories
{
    public interface IMenuGroupRepository : IRepository<MenuGroup>
    {

    }

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}