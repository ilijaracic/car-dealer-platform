using System;

namespace CarDealerPlatfromFunction.Exceptions
{
    internal class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException(string message) : base(message)
        {
        }
    }
}
