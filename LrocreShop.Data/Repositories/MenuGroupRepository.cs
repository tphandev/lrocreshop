using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;

namespace LrocreShop.Data.Repositories
{
    public interface IMenuGroupRepository
    {

    }

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}