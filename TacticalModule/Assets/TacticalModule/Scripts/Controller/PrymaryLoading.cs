﻿using TacticalModule.Scripts.Managers;
using TacticalModule.Scripts.Provider;
using UnityEngine;

namespace TacticalModule.Scripts.Controller
{
    public class PrymaryLoading : MonoBehaviour
    {
        private void Awake()
        {
            var provider = new GameProvider();
            ControllerTools.CreateMonoBehaviourController("IntetfaceController", true);
        }
    }
}