using System;

namespace TacticalModule.Scripts.Provider
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ProviderDetermine : Attribute
    {
        public IProvider Provider { get; private set; }
        public ProviderDetermine(IProvider providerType)
        {
            Provider = providerType;
        }
    }
}
