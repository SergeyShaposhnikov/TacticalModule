using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticalModule.Scripts.UI
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField]
        private List<BaseWindow> _openWinows = new List<BaseWindow>();

        public static InterfaceController Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public static BaseWindow OpenWindow(WinowType type)
        {
            var path = string.Format("Prefabs/UI/{0}Window", type.ToString());
            Debug.Log(path);
            var prefabWindow = Resources.Load(path, typeof(BaseWindow)) as BaseWindow;
            var windowInstanse = Instantiate(prefabWindow);
            windowInstanse.transform.SetParent(Instance.transform);
            windowInstanse.InitWindow(type);
            var rect = windowInstanse.GetComponent<RectTransform>();
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
            rect.localScale = Vector3.one;
            return windowInstanse;
        }

        public static void Close(WinowType type)
        {
            var window = Instance._openWinows.FirstOrDefault(wnd => wnd.Type == type);
            Close(window);
        }

        public static void Close(BaseWindow baseWindow)
        {
            Instance._openWinows.Remove(baseWindow);
            Destroy(baseWindow.gameObject);
        }
    }
}
