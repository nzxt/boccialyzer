using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Boccialyzer.Core.Repository
{
    public interface ISendGridRepository
    {
        Task<(OperationResult Result, int Value, string Message)> Send(string email, Guid invitationId, string locale);
    }
    public class SendGridRepository : ISendGridRepository
    {

        private readonly AppOptionModel _appOptions;
        private readonly IConfigurationRepository _configuration;
        private readonly SendGridModel _sendGridModel;
        private readonly UserManager<AppUser> _userManager;

        public SendGridRepository(IOptionsSnapshot<AppOptionModel> appOptions, IConfigurationRepository configuration, UserManager<AppUser> userManager)
        {
            _appOptions = appOptions.Value;
            _configuration = configuration;
            _userManager = userManager;
        }


        /// <inheritdoc/>
        public async Task<(OperationResult Result, int Value, string Message)> Send(string email, Guid invitationId, string locale)
        {
            try
            {
                string confirmEmailUrl = _appOptions.ConfirmEmailUrl;
                if (!string.IsNullOrEmpty(confirmEmailUrl))
                {
                    var _emailModel = await _configuration.GetSendGridOptions(locale);
                    if (_emailModel.Result == OperationResult.Ok)
                    {
                        var options = _emailModel.Value;
                        var bytesToHash = Encoding.UTF8.GetBytes(invitationId.ToString());
                        var hash64 = Convert.ToBase64String(bytesToHash);

                        options.CallbackUrl = QueryHelpers.AddQueryString(_appOptions.ConfirmEmailUrl, "code", hash64);
                        options.HtmlContent = options.HtmlContent.Replace("CALLBACKURL", options.CallbackUrl);
                        options.TextContent = options.TextContent.Replace("CALLBACKURL", options.CallbackUrl);

                        var client = new SendGridClient(_appOptions.SendGrid.ApiKey);
                        var from = new EmailAddress("sales-assessment-center@tbt-ua.com", "TBT-Ukraine");
                        var subject = options.Subject;
                        var to = new EmailAddress("antraxua@googlemail.com");//email);
                        var plainTextContent = options.TextContent;
                        var htmlContent = options.HtmlContent;
                        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                        var response = await client.SendEmailAsync(msg);
                    }
                }

                return (Result: OperationResult.Ok, Value: 0, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: 0, Message: ex.Message); }
        }
    }
}
