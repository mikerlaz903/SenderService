using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService
{
    public static class Constants
    {
        public static List<string> SupportedMessengers { get; } = new()
        {
            "telegram"
        };

        public static string ApplicableMessageVersion { get; } =
            "1.0.1.1";
    }
}
