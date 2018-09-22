using System;

namespace TacticalModule.Scripts.Provider
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ProviderRegistrer : Attribute
    {
        public Type Provider { get; private set; }
        public ProviderRegistrer(Type providerType)
        {
            Provider = providerType;
        }
    }
}