using System;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalModule.Scripts.UI
{
    public class LobbyUI : BaseWindow
    {
        [SerializeField] private Button _testButton;

        private void Awake()
        {
            Debug.Log("Show lobby window");
            _testButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {

        }
    }
}
