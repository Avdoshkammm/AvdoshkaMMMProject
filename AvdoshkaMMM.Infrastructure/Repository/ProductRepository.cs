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
            IEnumerable<Product> products = await db.Products.ToListAsync();
            try
            {
                return products;
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка вывода всех продуктов");
                return null;
            }
        }
        public async Task<Product> GetValue(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            try
            {
                return product;
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка вывода продукта");
                return null;
            }
        }
        public async Task CreateValue(Product product)
        {
            await db.Products.AddAsync(product);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка создания продукта");
            }
        }
        public async Task UpdateValue(Product product)
        {
            db.Products.Update(product);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка обновления продукта");
            }
        }
        public async Task DeleteValue(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message.ToString()}\nОшибка удаления продукта");
            }
        }
    }
}
