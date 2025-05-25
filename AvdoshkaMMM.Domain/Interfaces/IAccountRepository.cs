using AvdoshkaMMM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvdoshkaMMM.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Login(User user);
        Task<User> Register(User user, string password);
    }
}
