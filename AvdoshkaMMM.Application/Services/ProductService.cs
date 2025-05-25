using AutoMapper;
using AvdoshkaMMM.Application.DTO;
using AvdoshkaMMM.Application.Interfaces;
using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        public ProductService(ILogger<ProductService> _logger, IProductRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            logger = _logger;
            mapper = _mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetValues()
        {
            IEnumerable<Product> products = await repository.GetValues();
            try
            {
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ProductDTO> GetValue(int id)
        {
            Product product = await repository.GetValue(id);
            try
            {
                return mapper.Map<ProductDTO>(product);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task CreateValue(ProductDTO dto)
        {

        }
    }
}
