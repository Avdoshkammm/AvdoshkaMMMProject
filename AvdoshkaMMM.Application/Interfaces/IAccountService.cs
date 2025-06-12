using AvdoshkaMMM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvdoshkaMMM.Application.Interfaces
{
    public interface IAccountService
    {
        Task Register(UserDTO user, string password);
        Task Login(UserDTO userdto);
    }
}
