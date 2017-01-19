using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAsService.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<bool> Insert(T item);

        Task<bool> Update(T item);

        Task<bool> Delete(int id);
    }
}