using System;
using System.Linq;
using System.Reflection;
using TacticalModule.Scripts.Managers;
using TacticalModule.Scripts.Provider;

namespace Assets.TacticalModule.Scripts.Provider
{
    public static class ProviderTools
    {
        public static void RegistredController(IController controller)
        {
            var attribute = controller.GetType().GetCustomAttributes(typeof(ProviderRegistrer), true).First() as ProviderRegistrer;
            if (attribute == null)
            {
                throw new Exception("Registred attribute not found");
            }
            var info = attribute.Provider.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if (info == null)
            {
                throw new Exception(string.Format("Instance field not found for type {0}", attribute.Provider));
            }

            var instance = info.GetValue(null, null);
            var provider = instance as IProvider;
            provider.Register(controller);
        }
    }
}
