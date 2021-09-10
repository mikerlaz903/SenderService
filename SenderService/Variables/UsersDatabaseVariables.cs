using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Variables
{
    internal class UsersDatabaseVariables
    {
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
