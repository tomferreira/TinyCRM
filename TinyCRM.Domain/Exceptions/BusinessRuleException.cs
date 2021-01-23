using System;

namespace TinyCRM.Domain.Exceptions
{
    public class BusinessRuleException: Exception
    {
        public string Key { get; private set; }

        public BusinessRuleException(string key, string message)
            : base(message)
        {
            Key = key;
        }
    }
}
