using Json.Net;
using MessengerOptions;
using MessengerOptions.Factories;
using MessengerOptions.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SenderService.Administration;
using SenderService.Commands;
using SenderService.Exceptions;
using SenderService.Factories;
using SenderService.Models;
using SenderService.Variables;
using System;
using System.Text;

namespace SenderService
{
    public class Program
    {
        private static readonly Variables.Variables Variables = VariablesProvider.ProgramConstants;
        public static void Main(string[] args)
        {
            DbContextFactory.ChangeDatabaseLocation(Variables.UsersDatabase.Location);

            var messengerFactory = new MessengerFactory();
            var telegram = (TelegramMessenger.TelegramMessenger)messengerFactory.GetTelegramMessenger(Variables.Telegram);

            telegram.StartCheckingUpdates();

            var factory = new ConnectionFactory
            {
                HostName = VariablesProvider.ProgramConstants.RabbitMQ.RabbitHost,
                UserName = "guest",
                Password = "guest"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "messenger",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceiveMessageHandler;
            channel.BasicConsume(
                queue: "messenger",
                autoAck: true,
                consumer: consumer);


            OutputService.Write("Type [exit] to exit.", true, false, null);
            var consoleString = string.Empty;
            while (string.Compare(consoleString, "exit", StringComparison.OrdinalIgnoreCase) != 0)
            {
                Console.Write("> ");
                consoleString = Console.ReadLine();

                ConsoleCommandProcessor.ProcessHelp(consoleString);
                ConsoleCommandProcessor.ProcessShowUsers(consoleString);
                ConsoleCommandProcessor.ProcessDeleteUser(consoleString);
                ConsoleCommandProcessor.ProcessChangeDatabaseLocation(consoleString);
            }
        }

        private static void ReceiveMessageHandler(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var fullMessage = Encoding.UTF8.GetString(body);

            MessageModel message = default;
            try
            {
                message = ReadMessage(fullMessage);
            }
            catch (Exception exception)
            {
                OutputService.Write("Error was occurred while serialization JSON. " +
                                    "Probably it was throw cause different message version or incorrect metadata structure." +
                                    $"Inner exception: {exception}",
                    true, false, null);
            }

            if (message == null)
                return;

            if (string.CompareOrdinal(message.MessageVersion,
                Constants.ApplicableMessageVersion) != 0)
            {
                OutputService.Write($"Received messages metadata targeted {message.MessageVersion}." +
                                    $"Applicable version is {Constants.ApplicableMessageVersion}." +
                                    "Difference between versions might cause errors or flows in service work.",
                    true, false, null);
            }

            try
            {
                IMessenger messenger;
                string chatId;
                var factory = new MessengerFactory();
                switch (message.Messenger)
                {
                    case "telegram":
                        chatId = GetUserTelegramByIdHash(message.UserIdHash).TelegramUserId.ToString();
                        messenger = factory.GetTelegramMessenger(Variables.Telegram);
                        messenger.SendMessage(chatId, message.Text);
                        break;
                    case "whatsApp":
                        chatId = GetWhatsAppUserByIdHash(message.UserIdHash).WhatsAppUserId.ToString();
                        messenger = factory.GetTelegramMessenger(Variables.Telegram);
                        messenger.SendMessage(chatId, message.Text);
                        break;
                    default:
                        throw new NotSupportedMessengerException(
                            $"Received messages metadata targeted {message.Messenger}. " +
                            $"When supported messengers are {string.Join(",", Constants.SupportedMessengers)}");
                }

            }
            catch (Exception exception)
            {
                OutputService.Write("Error was occurred while sending message. " +
                                    $"Inner exception: {exception}",
                    true, false, null);
            }
        }

        private static MessageModel ReadMessage(string fullMessage)
        {
            return JsonNet.Deserialize<MessageModel>(fullMessage);
        }

        private static WhatsAppUserInfo GetWhatsAppUserByIdHash(string userIdHash)
        {
            using var context = new DbContextFactory().GetDbContext();
            return context.GetWhatsAppUserByIdHash(userIdHash);
        }

        private static TelegramUserInfo GetUserTelegramByIdHash(string userIdHash)
        {
            using var context = new DbContextFactory().GetDbContext();
            return context.GetTelegramUserByIdHash(userIdHash);
        }
    }
}
