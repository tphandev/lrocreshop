using System;

namespace LrocreShop.Data.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        LrocreShopDbContext Init();
    }
}