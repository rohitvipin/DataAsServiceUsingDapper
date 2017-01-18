using System.Data;

namespace DataAsService.DAL.Configuration.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetInstance();
    }
}