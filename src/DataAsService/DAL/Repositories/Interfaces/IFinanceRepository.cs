using System.Threading.Tasks;
using DataAsService.DAL.Models;

namespace DataAsService.DAL.Repositories.Interfaces
{
    public interface IFinanceRepository : IRepository<FinanceCombined>
    {
        Task<FinanceCombined> GetById(string id);
    }
}