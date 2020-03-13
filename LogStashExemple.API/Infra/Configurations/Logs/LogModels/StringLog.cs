using System.Diagnostics.CodeAnalysis;

namespace LogStashExemple.API.Infra.Configurations.Logs.LogModels
{
    [ExcludeFromCodeCoverage]
    public class StringLog
    {
        private StringLog(string value) => StringValue = value;

        public static StringLog GetLog(string value) => new StringLog(value);
        public string StringValue { get; set; }
    }
}
