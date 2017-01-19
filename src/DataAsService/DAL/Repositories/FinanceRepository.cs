using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAsService.DAL.Configuration.Interfaces;
using DataAsService.DAL.Models;
using DataAsService.DAL.Repositories.Interfaces;

namespace DataAsService.DAL.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private const string SqlQueryGetAll = @"DECLARE @startFMMonthNumber INT

                                            SELECT @startFMMonthNumber = MONTH(FiscalMonth + '1 2016')
                                            FROM PrefGen;

                                            WITH months (MonthNumber)
                                            AS (
	                                            SELECT @startFMMonthNumber
	
	                                            UNION ALL
	
	                                            SELECT MonthNumber + 1
	                                            FROM months
	                                            WHERE MonthNumber < 11
	                                            )

                                            SELECT unpvtAcctBal.Id, curbal AS CurrentBalance, begbal AS BeginingBalance, plcat AS PLCategory, unpvtAcctBal.MonthValue AS AcctBalMonthValue, unpvtAcctbudgfc.MonthValue AS AcctbudgfcMonthValue, m.Id AS MonthId, m.[MonthName]
                                            FROM (
	                                            SELECT ab.id, ab.curbal, ab.begbal, ab.month1, ab.month2, ab.month3, ab.month4, ab.month5, ab.month6, ab.month7, ab.month8, ab.month9, ab.month10, ab.month11, ab.month12, ap.plcat
	                                            FROM AcctBal ab
	                                            INNER JOIN AcctProf ap ON ab.id = ap.id
	                                            ) p
                                            UNPIVOT(MonthValue FOR MonthId IN (month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12)) AS unpvtAcctBal
                                            INNER JOIN (
	                                            SELECT id, MonthValue, MonthId
	                                            FROM (
		                                            SELECT id, abf.month1, abf.month2, abf.month3, abf.month4, abf.month5, abf.month6, abf.month7, abf.month8, abf.month9, abf.month10, abf.month11, abf.month12
		                                            FROM acctbudgfc abf
		                                            ) p
	                                            UNPIVOT(MonthValue FOR MonthId IN (month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12)) AS unpvt
	                                            ) AS unpvtAcctbudgfc ON unpvtAcctbudgfc.id = unpvtAcctBal.id
	                                            AND unpvtAcctbudgfc.MonthId = unpvtAcctBal.MonthId
                                            INNER JOIN (
	                                            SELECT DATENAME(MONTH, DATEADD(MONTH, MonthNumber - 1, GETDATE())) AS [MonthName], 'month' + cast(MonthNumber AS CHAR) AS [Id]
	                                            FROM months
	                                            ) AS m ON unpvtAcctBal.MonthId = m.Id ";

        private const string SqlQueryGetById = SqlQueryGetAll + "WHERE unpvtAcctBal.Id = @AcctBalId";

        private readonly IDbConnection _dbConnection;

        public FinanceRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory?.GetInstance();
        }

        public async Task<IEnumerable<FinanceCombined>> Get()
        {
            var queryAsync = _dbConnection.QueryAsync<FinanceCombined>(SqlQueryGetAll);
            return queryAsync != null ? await queryAsync : null;
        }

        public async Task<FinanceCombined> GetById(string id)
        {
            var queryAsync = _dbConnection.QueryAsync<FinanceCombined>(SqlQueryGetById, new { AcctBalId = id });
            return queryAsync != null ? (await queryAsync).SingleOrDefault() : null;
        }

        public Task<FinanceCombined> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(FinanceCombined item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(FinanceCombined item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
