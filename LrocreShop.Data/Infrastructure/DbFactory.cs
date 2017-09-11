namespace LrocreShop.Data.Infrastructure
{
    public  class DbFactory : Disposable, IDbFactory
    {
        private LrocreShopDbContext dbContext;

        public LrocreShopDbContext Init()
        {
            return dbContext ?? (dbContext = new LrocreShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}