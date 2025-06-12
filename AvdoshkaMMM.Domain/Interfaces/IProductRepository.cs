using AvdoshkaMMM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvdoshkaMMM.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetValues();
        Task<Product> GetValue(int id);
        Task CreateValue(Product product);
        void UpdateValue(Product product);
        Task DeleteValue(int id);
    }
}
