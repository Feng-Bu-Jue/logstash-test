﻿using System.Diagnostics.CodeAnalysis;

namespace LogStashExemple.API.Infra.Configurations.Logs.LogModels
{
    [ExcludeFromCodeCoverage]
    public class BoolLog
    {
        private BoolLog(bool value) => BoolValue = value;

        public static BoolLog GetLog(bool value) => new BoolLog(value);
        public bool BoolValue { get; set; }
    }
}
