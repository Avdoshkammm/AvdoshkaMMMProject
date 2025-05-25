using AutoMapper;
using AvdoshkaMMM.Application.DTO;
using AvdoshkaMMM.Application.Interfaces;
using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<AccountService> logger;
        public AccountService(IAccountRepository _repository, IMapper _mapper, ILogger<AccountService> _logger)
        {
            repository = _repository;
            mapper = _mapper;
            logger = _logger;
        }
        public async Task Login(UserDTO user)
        {
            User servUser = mapper.Map<User>(user);
            try
            {
                await repository.Login(servUser);
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в сервисе. Метод Login.\nОписание: {ex.Message}");
            }
        }

        public async Task Register(UserDTO user, string password)
        {
            User servUser = mapper.Map<User>(user);
            try
            {
                await repository.Register(servUser, password);
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка в сервисе. Метод Register.\nОписание: {ex.Message}");
            }
        }
    }
}
