﻿namespace CarDealerPlatform.Domain.Exceptions
{
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException(string message) : base(message)
        {
        }
    }
}
