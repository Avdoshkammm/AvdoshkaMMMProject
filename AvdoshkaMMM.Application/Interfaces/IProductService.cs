using AvdoshkaMMM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvdoshkaMMM.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetValues();
        Task<ProductDTO> GetValue(int id);
        Task<ProductDTO> CreateValue(ProductDTO productDTO);
        Task<ProductDTO> UpdateValue(int id, ProductDTO productDTO);
        Task DeleteValue(int id);
    }
}
