using Json.Net;
using SenderService.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Variables
{
    internal static class VariablesProvider
    {
        private const string ConstantsFile = "constants.json";
        public static Variables ProgramConstants { get; private set; }

        static VariablesProvider()
        {
            ProgramConstants = ReadConstants();
        }

        public static void Refresh()
        {
            ProgramConstants = ReadConstants();
        }
        private static Variables ReadConstants()
        {
            return JsonNet.Deserialize<Variables>(ResourceJsonHandler.ReadResource(ConstantsFile));
        }

        public static void SaveConstants()
        {
            var newJson = JsonNet.Serialize(ProgramConstants);
            File.WriteAllText(ConstantsFile, newJson);
        }
    }
}
