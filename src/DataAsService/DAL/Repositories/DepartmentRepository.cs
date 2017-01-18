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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _dbConnection;

        private const string TableName = "[DEPARTMT]";
        private const string IdColumn = " [UniqueID]";
        private const string AllTableColumnsExceptId = @" [DEPARTMENT]
                                                         ,[DEPTNAME]
                                                         ,[LOCATION]
                                                         ,[SCALE]
                                                         ,[NUTRIENT]
                                                         ,[DISCOUNT]
                                                         ,[QUANTITY]
                                                         ,[Category]
                                                         ,[Classification1]
                                                         ,[Classification2]
                                                         ,[Classification3]
                                                         ,[Classification4]
                                                         ,[Classification5]
                                                         ,[Classification6]
                                                         ,[XRef1]
                                                         ,[XRef2]
                                                         ,[XRef3]
                                                         ,[XRef4]
                                                         ,[MissingLotNumWarnType]
                                                         ,[InvalidLotNumWarnType]
                                                         ,[GLqtyUOM]      
                                                         ,[PriceDA]
                                                         ,[QtyDA]
                                                         ,[CostDA]
                                                         ,[LogPriceOverrides]
                                                         ,[RequirePriceOverrideReason]
                                                         ,[QtySellWarningMethod]
                                                         ,[discountbooklines]
                                                         ,[DiscDwnPay]
                                                         ,[AutoUpdatePricingOnCostChange]
                                                         ,[DiscDwnBook] ";
        private const string AllTableColumns = IdColumn + " , " + AllTableColumnsExceptId;
        private const string AllParametersExceptId = @"@DEPARTMENT,@DEPTNAME,@LOCATION,@SCALE,@NUTRIENT,@DISCOUNT,@QUANTITY,@Category,@Classification1,@Classification2,@Classification3,@Classification4,@Classification5,@Classification6,@XRef1,@XRef2,@XRef3,@XRef4,@MissingLotNumWarnType,@InvalidLotNumWarnType,@GLqtyUOM,@PriceDA,@QtyDA,@CostDA,@LogPriceOverrides,@RequirePriceOverrideReason,@QtySellWarningMethod,@discountbooklines,@DiscDwnPay,@AutoUpdatePricingOnCostChange,@DiscDwnBook";

        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory?.GetInstance();
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var queryAsync = _dbConnection.QueryAsync<Department>($@"SELECT {AllTableColumns} FROM {TableName}");
            return queryAsync != null ? await queryAsync : null;
        }

        public async Task<Department> GetById(int id)
        {
            var queryAsync = _dbConnection.QueryAsync<Department>($"SELECT {AllTableColumns} FROM {TableName} WHERE {IdColumn} = @DepartmentID", new { DepartmentID = id });
            return queryAsync != null ? (await queryAsync).SingleOrDefault() : null;
        }

        public async Task<bool> Insert(Department item)
        {
            var executeAsync = _dbConnection.ExecuteAsync($"INSERT {TableName} ({AllTableColumnsExceptId}) VALUES ({AllParametersExceptId})", item);

            if (executeAsync == null)
            {
                return false;
            }

            var rowsAffected = await executeAsync;
            return rowsAffected > 0;
        }

        public async Task<bool> Update(Department item)
        {
            if (item == null)
            {
                return false;
            }

            var executeAsync = _dbConnection.ExecuteAsync($"UPDATE {TableName} SET [DEPARTMENT]=@Department,[DEPTNAME]=@DeptName,[LOCATION]=@Location WHERE {IdColumn} = @DepartmentID"
                , new { DepartmentID = item.UniqueID, DeptName = item.DEPARTMENT, Location = item.LOCATION });

            if (executeAsync == null)
            {
                return false;
            }

            var rowsAffected = await executeAsync;
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var executeAsync = _dbConnection.ExecuteAsync($"DELETE FROM {TableName} WHERE {IdColumn} = @DepartmentID", new { DepartmentID = id });

            if (executeAsync == null)
            {
                return false;
            }

            var rowsAffected = await executeAsync;
            return rowsAffected > 0;
        }
    }
}