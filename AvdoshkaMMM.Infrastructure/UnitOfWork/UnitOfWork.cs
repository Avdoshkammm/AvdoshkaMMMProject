using AvdoshkaMMM.Domain.Interfaces;
using AvdoshkaMMM.Infrastructure.Data;
using AvdoshkaMMM.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AvdoshkaMMMDbContext db;
        private readonly ILoggerFactory logger;
        private IProductRepository? productRepository;
        public UnitOfWork(AvdoshkaMMMDbContext _db, ILoggerFactory _logger)
        {
            db = _db;
            logger = _logger;
        }

        public IProductRepository Products
        {
            get
            {
                if(productRepository == null)
                {
                    var repoLogger = logger.CreateLogger<ProductRepository>();
                    productRepository = new ProductRepository(db, repoLogger);
                }
                return productRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
