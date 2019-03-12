using Boccialyzer.Domain.Enums;
using Dapper;
using Npgsql;
using Serilog;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій SystemJob
    /// </summary>
    public interface ISystemJobRepository
    {
        #region # Task<(OperationResult Result, int Value, string Message)> RunIssueStatusUpdate(string connectionString)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        Task<(OperationResult Result, int Value, string Message)> RunIssueStatusUpdate(string connectionString);

        #endregion
    }

    /// <summary>
    /// Репозиторій SystemJob
    /// </summary>
    public class SystemJobRepository : ISystemJobRepository
    {

        /// <summary>
        /// Рядок підключення до БД
        /// </summary>
        //private readonly string _connectionString;

        public SystemJobRepository() //string connectionString)
        {
            //_connectionString = connectionString;
        }

        #region # Task<(OperationResult Result, int Value, string Message)> RunIssueStatusUpdate(string connectionString)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, int Value, string Message)> RunIssueStatusUpdate(
            string connectionString)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    var result = await db.QueryFirstAsync<int>("public.athena_upd_issue_status_in_midnight",
                        commandType: CommandType.StoredProcedure);
                    db.Close();
                    return (Result: OperationResult.Ok, Value: result, Message: "");
                }
                catch (Exception ex)
                {
                    Log.Error("{SqlError}", ex.Message);
                    return (Result: OperationResult.Error, Value: 0, Message: ex.Message);
                }
            }
        }

        #endregion
    }
}