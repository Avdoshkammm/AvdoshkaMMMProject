using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Domain.Interfaces;
using AvdoshkaMMM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AvdoshkaMMMDbContext db;
        private readonly ILogger<ProductRepository> logger;
        public ProductRepository(AvdoshkaMMMDbContext _db, ILogger<ProductRepository> _logger)
        {
            db = _db;
            logger = _logger;
        }
        public async Task<IEnumerable<Product>> GetValues()
        {
            try
            {
                IEnumerable<Product> products = await db.Products.AsNoTracking().ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка вывода продуктов");
                throw;
            }
        }
        public async Task<Product> GetValue(int id)
        {
            try
            {
                Product? product = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
                return product;
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex}\nОшибка вывода продукта с ID {id}");
                throw;
            }
        }
        public async Task CreateValue(Product product)
        {
            try
            {
                await db.Products.AddAsync(product);
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка создания продукта");
                throw;
            }
        }
        public void UpdateValue(Product product)
        {
            try
            {
                db.Products.Update(product);
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка обновления продукта");
                throw;
            }
        }
        public async Task DeleteValue(int id)
        {
            try
            {
                Product? product = await db.Products.FindAsync(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}\nОшибка удаления продукта");
                throw;
            }
        }
    }
}