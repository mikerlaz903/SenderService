using SenderService.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace SenderService.Messengers
{
    internal class TelegramMessenger : Messenger
    {
        private static QueuedUpdateReceiver _receiver;
        private static TelegramBotClient _client;

        private string _urlTemplate = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";

        public override string UrlTemplate
        {
            get => string.Format(_urlTemplate, UrlTemplateData.Values.ToArray());
            set => _urlTemplate = value;
        }

        public override Dictionary<string, string> UrlTemplateData { get; set; } = new();

        public TelegramMessenger(string message = "")
        {
            SetUrlTemplate(message);
            SetReceiver();
        }

        private void SetUrlTemplate(string message)
        {
            var constants = VariablesProvider.ProgramConstants;

            UrlTemplateData.Add("telegramApiToken", constants.Telegram.telegramApiToken);
            UrlTemplateData.Add("telegramChatId", "");
            UrlTemplateData.Add("telegramMessage", message);
        }

        private void SetReceiver()
        {
            var constants = VariablesProvider.ProgramConstants;

            _client = new TelegramBotClient(constants.Telegram.telegramApiToken);
            _receiver = new QueuedUpdateReceiver(_client);
            _receiver.StartReceiving();
        }

        public void SendMessage(string to, string message = null)
        {
            UrlTemplateData["telegramChatId"] = to;
            UrlTemplateData["telegramMessage"] = message ?? ReturningMessage;
            if (ReturningMessage == string.Empty)
                return;

            SendMessage();
        }
    }
}
