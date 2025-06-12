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
        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ProductService(ILogger<ProductService> _logger, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            logger = _logger;
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> GetValues()
        {
            IEnumerable<Product> products = await unitOfWork.Products.GetValues();
            try
            {
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в маппере ProductService, GetValues() : {ex.Message}");
                return Enumerable.Empty<ProductDTO>();
            }
        }

        public async Task<ProductDTO> GetValue(int id)
        {
            if(id == null)
            {
                logger.LogError("В сервис передан пустой ID");
                return null;
            }
            Product product = await unitOfWork.Products.GetValue(id);
            try
            {
                return mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка в маппере ProductService, GetValue(int id) : {ex.Message}");
                return null;
            }
        }

        public async Task<ProductDTO> CreateValue(ProductDTO productDTO)
        {
            Product newProduct = mapper.Map<Product>(productDTO);
            await unitOfWork.Products.CreateValue(newProduct);
            await unitOfWork.SaveChangesAsync();
            try
            {
                return mapper.Map<ProductDTO>(newProduct);
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в маппере ProductService, CreateValue : {ex.Message}");
                return null;
            }
        }

        public async Task<ProductDTO> UpdateValue(int id, ProductDTO productDTO)
        {
            Product updateProduct = mapper.Map<Product>(productDTO);
            unitOfWork.Products.UpdateValue(updateProduct);
            await unitOfWork.SaveChangesAsync();
            try
            {
                return mapper.Map<ProductDTO>(updateProduct);
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в маппере ProductService, UpdateValue : {ex.Message}");
                return null;
            }
        }

        public async Task DeleteValue(int id)
        {
            try
            {
                await unitOfWork.Products.DeleteValue(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в маппере ProductService, DeleteValue : {ex.Message}");
            }
        }
    }
}