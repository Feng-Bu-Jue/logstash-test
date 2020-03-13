﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace LogStashExemple.API.Infra.Configurations.Logs.LogModels
{
    [ExcludeFromCodeCoverage]
    public class GuidLog
    {
        private GuidLog(Guid value) => GuidValue = value;

        public static GuidLog GetLog(Guid value) => new GuidLog(value);

        public Guid GuidValue { get; set; }
    }
}
