using SenderService.Variables.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessenger.Variables;
using WhatsAppMessenger.Variables;

namespace SenderService.Variables
{
    internal class Variables
    {
        public TelegramVariables Telegram { get; set; }
        public WhatsAppVariables WhatApp { get; set; }
        public RabbitMQVariables RabbitMQ { get; set; }
        public UsersDatabaseVariables UsersDatabase { get; set; }
    }
}
