using SenderService.Administration;
using SenderService.Commands;
using SenderService.Factories;
using SenderService.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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

        public async void StartCheckingUpdates()
        {
            var updates = _receiver.YieldUpdatesAsync();
            await foreach (var update in updates)
            {
                if (update.Message == null)
                    continue;

                var sender = update.Message.From;
                var message = update.Message.Text;

                ProcessUserMessage(message, sender);
                SendMessage(sender.Id.ToString());
            }
        }

        private bool IsAllowedUsers(User sender)
        {
            return AllowedUsers.UserList.Any(dbUsers => dbUsers.TelegramUserId == sender.Id);
        }

        private void ProcessUserMessage(string message, User sender)
        {
            ReturningMessage = "Command not recognized or unauthorized user";

            if (!IsAllowedUsers(sender))
            {
                MessageCommandProcessor.MatchStart(message, this);
                ProcessNewUsers(sender);

                return;
            }

            MessageCommandProcessor.MatchAddNewUserRequest(message, this);
            MessageCommandProcessor.MatchShowRole(message, sender, this);
        }

        private void ProcessNewUsers(User sender)
        {
            var adminMessage = $"Unauthorized user ({sender.Id}) try to interact.";

            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();

            var adminChat = context.GetAdministrator().TelegramUserId;

            TelegramBotAdministration.SendTelegramPermission(sender.Id, sender.Username, this, adminChat, adminMessage);
            OutputService.Write(adminMessage, true, false, null);
        }

        public async void SendTelegramMessage(ChatId id, string text, ReplyMarkupBase markup)
        {
            await _client.SendTextMessageAsync(id, text, ParseMode.Default, null, false, false, 0, false, markup);
        }
    }
}
