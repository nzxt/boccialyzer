using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        Task<(OperationResult Result, SendGridEmailModel Value, string Message)> GetSendGridOptions(string locale);
        Task<string> GetTermOfUse(string locale);
    }

    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # ConfigurationRepository constructor

        public ConfigurationRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        /// <inheritdoc/>
        public async Task<(OperationResult Result, SendGridEmailModel Value, string Message)> GetSendGridOptions(string locale)
        {
            try
            {
                var fromDb = await _dbContext.Configurations.FirstOrDefaultAsync(x => x.Name == "SendGridOptions");
                if (fromDb == null)
                    return (Result: OperationResult.Error, Value: null, Message: "Налаштування SendGrid не знайдені.");
                var appSettingsDefinition = new
                {
                    SendGridOptions = new {
                        Subject = new[] { new { Locale = string.Empty, Text = string.Empty }},
                        HtmlContent = new[] { new { Locale = string.Empty, Text = string.Empty }},
                        TextContent = new[] { new { Locale = string.Empty, Text = string.Empty }}
                }
                };
                var sendGridOptions = JsonConvert.DeserializeAnonymousType(fromDb.Value, appSettingsDefinition).SendGridOptions;
                SendGridEmailModel result = new SendGridEmailModel();
                result.Subject= sendGridOptions.Subject.SingleOrDefault(x => x.Locale == locale)?.Text;
                result.HtmlContent = sendGridOptions.HtmlContent.SingleOrDefault(x => x.Locale == locale)?.Text;
                result.TextContent= sendGridOptions.TextContent.SingleOrDefault(x => x.Locale == locale)?.Text;


                return (Result: OperationResult.Ok, Value: result, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        /// <inheritdoc/>
        public async Task<string> GetTermOfUse(string locale)
        {
            var termOfUse = await _dbContext.Configurations.FirstOrDefaultAsync(x => x.Name == "TermOfUse");

            var appSettingsDefinition = new
            {
                TermOfUse = new[] { new { Locale = string.Empty, Text = string.Empty } }
            };

            var termOfUseText = JsonConvert.DeserializeAnonymousType(termOfUse.Value, appSettingsDefinition)
                .TermOfUse.SingleOrDefault(x => x.Locale == locale)?.Text;
            return termOfUseText;
        }

    }
}
