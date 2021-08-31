using Json.Net;
using SenderService.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Variables
{
    internal static class VariablesProvider
    {
        public static Variables ProgramConstants { get; }

        static VariablesProvider()
        {
            ProgramConstants = ReadConstants();
        }

        private static Variables ReadConstants()
        {
            return JsonNet.Deserialize<Variables>(ResourceJsonHandler.ReadResource("constants.json"));
        }
    }
}
