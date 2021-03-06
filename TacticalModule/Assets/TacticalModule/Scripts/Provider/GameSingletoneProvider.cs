﻿using System;
using System.Collections.Generic;
using TacticalModule.Scripts.Managers;

namespace TacticalModule.Scripts.Provider
{
    public class GameSingletoneProvider : IProvider
    {
        public static GameSingletoneProvider Instance { get; private set; }

        private readonly Dictionary<string, IController> _managers = new Dictionary<string, IController>();

        public GameSingletoneProvider()
        {
            Instance = this;
        }

        public void Register(IController newManager)
        {
            var managers = Instance._managers;
            var managerKey = newManager.GetType().ToString();
            if (managers.ContainsKey(managerKey))
            {
                throw new Exception(string.Format("Manager {0} allready registred", managerKey));
            }
            managers.Add(managerKey, newManager);
            UnityEngine.Debug.LogFormat("Registred manager {0}", newManager.GetType());
        }

        public void Unregister(IController destroyManager)
        {
            var managerKey = destroyManager.GetType().ToString();
            if (!_managers.ContainsKey(managerKey))
            {
                throw new Exception(string.Format("Manager {0} alreade unregistred", managerKey));
            }
            _managers.Remove(managerKey);
        }
    }
}
