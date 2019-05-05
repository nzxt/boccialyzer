using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Serilog;

namespace Boccialyzer.Core.Repository
{
    public interface IStatRepository
    {
        Task<(OperationResult Result, IEnumerable<StatResult> Value, string Message)> GetStatistic(StatParam param);
    }

    public class StatRepository : IStatRepository
    {
        #region # Local vars

        private readonly AppOptionModel _appOptions;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # DbOperation constructor

        public StatRepository(IOptionsSnapshot<AppOptionModel> appOptions, IUserInfo userInfo)
        {
            _appOptions = appOptions.Value;
            _userInfo = userInfo;
        }

        #endregion

        public async Task<(OperationResult Result, IEnumerable<StatResult> Value, string Message)> GetStatistic(StatParam param)
        {
            using (IDbConnection db = new NpgsqlConnection(_appOptions.ConnectionString))
            {
                try
                {
                    db.Open();
                    var result = await db.QueryAsync<StatResult>("public.boccialyzer_get_dashboard_stat", new
                    {
                        createdby = _userInfo.AppUserId,
                        playerid = param.PlayerId,
                        datefrom = param.DateFrom,
                        dateto = param.DateTo,
                        competitionevent = param.CompetitionEvent,
                        side = param.Side,
                        jackdistance = param.JackDistance,
                        eliminationstage = param.EliminationStage,
                        matchtype = param.MatchType,
                        tournamentid = param.TournamentId,
                        tournamenttypeid = param.TournamentTypeId,
                        matchid = param.MatchId,
                        stageid = param.StageId
                    }, commandType: CommandType.StoredProcedure);
                    db.Close();
                    if (result == null)
                        return (Result: OperationResult.Error, Value: null, Message: "[GetStatistic] Щось пішло не за планом...");
                    return (Result: OperationResult.Ok, Value: result, Message: "");
                }
                catch (Exception ex)
                {
                    Log.Error("{SqlError}", ex.Message);
                    return (Result: OperationResult.Error, Value: null, Message: ex.Message);
                }
            }
        }
    }
}
