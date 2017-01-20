using System.Collections.Generic;
using System.Threading.Tasks;
using DataAsService.DAL.Models;

namespace DataAsService.DAL.Repositories.Interfaces
{
    public interface IFinanceRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetByAcctBalId(string id);
    }
}