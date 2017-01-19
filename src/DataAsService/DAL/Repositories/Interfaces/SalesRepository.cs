using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataAsService.DAL.Configuration.Interfaces;
using DataAsService.DAL.Models;

namespace DataAsService.DAL.Repositories.Interfaces
{
    public class SalesRepository : ISalesRepository
    {
        private const string SqlQueryGetAll = @"SELECT departmt.category AS Category, invli.growshare AS GrowShare, invli.deptid AS DepartmentId, invli.prodid AS ProductId, product.prodname AS ProductName, invli.costshare AS CostShare, ltrim(grower.growname1 + ' ' + grower.growname2) AS 'Grower', location.NAME AS LocationName, round(invli.growshare - invli.costshare, 2) AS 'Profit', salesmen.firstname + ' ' + salesmen.lastname AS 'SalesPerson'
                                                FROM invoice
                                                INNER JOIN invli ON invoice.invnum = invli.invnum AND invoice.invdate = invli.invoicedate AND invli.location = invoice.location
                                                INNER JOIN grower ON grower.growid = invli.custid
                                                INNER JOIN departmt ON departmt.department = invli.deptid
                                                INNER JOIN product ON product.departid = invli.deptid AND product.prodid = invli.prodid
                                                INNER JOIN location ON location.locatid = invli.location
                                                LEFT JOIN salesmen ON salesmen.id = invoice.SalesmanID
                                                WHERE invoice.voided = 0 AND invoice.EndOfYearGUID IS NULL ";

        private readonly IDbConnection _dbConnection;

        public SalesRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory?.GetInstance();
        }

        public async Task<IEnumerable<SalesCombined>> Get()
        {
            var queryAsync = _dbConnection.QueryAsync<SalesCombined>(SqlQueryGetAll);
            return queryAsync != null ? await queryAsync : null;
        }

        public Task<SalesCombined> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(SalesCombined item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SalesCombined item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
