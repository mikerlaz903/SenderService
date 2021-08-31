using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Json
{
    public static class ResourceJsonHandler
    {
        public static string ReadResource(string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            var parentDirectory = Directory.GetParent(assembly.Location)?.FullName;
            var resourcePath = Path.Combine(parentDirectory ?? string.Empty, name);

            using var reader = new StreamReader(resourcePath);
            return reader.ReadToEnd();
        }
    }
}
