using System.Data;
using System.Data.SqlClient;
using DataAsService.DAL.Configuration.Interfaces;
using DataAsService.Services.Interfaces;

namespace DataAsService.DAL.Configuration
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IApplicationContextService _applicationContextService;

        public SqlConnectionFactory(IApplicationContextService applicationContextService)
        {
            _applicationContextService = applicationContextService;
        }

        public IDbConnection GetInstance()
        {
            if (string.IsNullOrEmpty(_applicationContextService?.ConnectionString))
            {
                return null;
            }

            var conn = new SqlConnection(_applicationContextService.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
