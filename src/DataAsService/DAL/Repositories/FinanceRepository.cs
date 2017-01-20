using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataAsService.DAL.Configuration.Interfaces;
using DataAsService.DAL.Models;
using DataAsService.DAL.Repositories.Interfaces;

namespace DataAsService.DAL.Repositories
{
    /// <summary>
    /// FinanceRepository
    /// </summary>
    public class FinanceRepository : IFinanceRepository
    {
        private const string SqlQueryGetAll = @"DECLARE @startFMMonthNumber INT
                                                DECLARE @currentFMMonthNumber INT

                                                SELECT @currentFMMonthNumber = ([Message] + 1)
                                                FROM StatMess
                                                WHERE Id = 'LastMonthClosed'

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
                                                SELECT unpvtAcctBal.Id AS AccountId, plcat AS PlCategory, curbal AS CurrentBalance, unpvtAcctBal.EndBalance, unpvtAcctbudgfc.ForecastBalance, m.[MonthName] AS [Month]
                                                , CASE WHEN m.MonthNumber > @currentFMMonthNumber THEN 'F' WHEN m.MonthNumber < @currentFMMonthNumber THEN 'P' WHEN m.MonthNumber = @currentFMMonthNumber THEN 'C' ELSE '0' END AS MonthFlag
                                                FROM (
	                                                SELECT ab.id, ab.curbal, ab.month1, ab.month2, ab.month3, ab.month4, ab.month5, ab.month6, ab.month7, ab.month8, ab.month9, ab.month10, ab.month11, ab.month12, ap.plcat
	                                                FROM AcctBal ab
	                                                INNER JOIN AcctProf ap ON ab.id = ap.id
	                                                ) p
                                                UNPIVOT(EndBalance FOR MonthId IN (month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12)) AS unpvtAcctBal
                                                INNER JOIN (
	                                                SELECT id, ForecastBalance, MonthId
	                                                FROM (
		                                                SELECT id, abf.month1, abf.month2, abf.month3, abf.month4, abf.month5, abf.month6, abf.month7, abf.month8, abf.month9, abf.month10, abf.month11, abf.month12
		                                                FROM acctbudgfc abf
		                                                ) p
	                                                UNPIVOT(ForecastBalance FOR MonthId IN (month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12)) AS unpvt
	                                                ) AS unpvtAcctbudgfc ON unpvtAcctbudgfc.id = unpvtAcctBal.id
	                                                AND unpvtAcctbudgfc.MonthId = unpvtAcctBal.MonthId
                                                INNER JOIN (
	                                                SELECT DATENAME(MONTH, DATEADD(MONTH, MonthNumber - 1, GETDATE())) AS [MonthName], 'month' + cast(MonthNumber AS CHAR) AS [Id], MonthNumber
	                                                FROM months
	                                                ) AS m ON unpvtAcctBal.MonthId = m.Id ";

        private const string SqlQueryGetById = SqlQueryGetAll + "WHERE unpvtAcctBal.Id = @AcctBalId";

        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionFactory"></param>
        public FinanceRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory?.GetInstance();
        }

        /// <summary>
        /// Get all finance records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> Get()
        {
            var queryAsync = _dbConnection.QueryAsync<Account>(SqlQueryGetAll);
            return queryAsync != null ? await queryAsync : null;
        }

        /// <summary>
        /// Get finance records by Account id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetByAcctBalId(string id)
        {
            var queryAsync = _dbConnection.QueryAsync<Account>(SqlQueryGetById, new { AcctBalId = id });
            return queryAsync != null ? await queryAsync : null;
        }

        public Task<Account> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Account item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Account item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
