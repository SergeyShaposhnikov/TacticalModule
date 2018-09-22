using System;
using System.Linq;
using System.Reflection;
using TacticalModule.Scripts.Provider;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TacticalModule.Scripts.Managers
{
    public static class ControllerTools
    {
        public static void CreateMonoBehaviourController(string managerName, bool dontDestroyOnLoad, Transform parent = null)
        {
            var prefab = Resources.Load<GameObject>(string.Format("Prefab/Controller/{0}", managerName));
            var newController = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            newController.name = managerName;
            if (dontDestroyOnLoad)
            {
                Object.DontDestroyOnLoad(newController.gameObject);
            }

            var controller = newController.GetComponent<IController>();

            var attribute = controller.GetType().GetCustomAttributes(typeof(ProviderRegistrer), true).First() as ProviderRegistrer;
            if (attribute == null)
            {
                throw new Exception("Registred attribute not found");
            }
            var info = attribute.Provider.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if (info == null)
            {
                throw new Exception(string.Format("Instance field not found for type {0}", attribute.Provider.GetType()));
            }

            var instance = info.GetValue(null, null);
            var provider = instance as IProvider;
            provider.Register(controller);
        }
    }
}