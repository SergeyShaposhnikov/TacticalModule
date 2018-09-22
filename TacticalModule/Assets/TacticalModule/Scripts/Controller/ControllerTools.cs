using System;
using System.Linq;
using System.Reflection;
using Assets.TacticalModule.Scripts.Provider;
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
            ProviderTools.RegistredController(controller);
        }
    }
}