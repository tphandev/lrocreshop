using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LrocreShop.Data.Infrastructure;
using LrocreShop.Model.Models;

namespace LrocreShop.Data.Repositories
{
    public interface IContactDetailRepository : IRepository<ContactDetail>
    {
    }

    public class ContactDetailRepository : RepositoryBase<ContactDetail>, IContactDetailRepository
    {
        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
