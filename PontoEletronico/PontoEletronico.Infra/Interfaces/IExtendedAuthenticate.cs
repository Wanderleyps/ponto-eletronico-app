using PontoEletronico.Domain.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Infra.Data.Interfaces
{
    public interface IExtendedAuthenticate : IAuthenticate
    {
        Task<string> GetUserIdByEmailAsync(string email);
    }
}
