using System.Collections.Generic;
using TacticalModule.Scripts.Managers;
using TacticalModule.Scripts.Provider;
using TacticalModule.Scripts.UI;
using UnityEngine;

namespace TacticalModule.Scripts.Controller
{
    [ProviderRegistrer(typeof(GameProvider))]
    public class InterfaceController : MonoBehaviour, IController
    {
        [SerializeField]
        private List<BaseWindow> _openWinows = new List<BaseWindow>();

        //public static BaseWindow OpenWindow(WinowType type)
        //{
        //    //var path = string.Format("Prefabs/UI/{0}Window", type.ToString());
        //    //Debug.Log(path);
        //    //var prefabWindow = Resources.Load(path, typeof(BaseWindow)) as BaseWindow;
        //    //var windowInstanse = Instantiate(prefabWindow);
        //    //windowInstanse.transform.SetParent(Instance.transform);
        //    //windowInstanse.InitWindow(type);
        //    //var rect = windowInstanse.GetComponent<RectTransform>();
        //    //rect.sizeDelta = Vector2.zero;
        //    //rect.anchoredPosition = Vector2.zero;
        //    //rect.localScale = Vector3.one;
        //    //return windowInstanse;
        //}

        public static void Close(WinowType type)
        {
            //var window = Instance._openWinows.FirstOrDefault(wnd => wnd.Type == type);
            //Close(window);
        }

        public static void Close(BaseWindow baseWindow)
        {
            //Instance._openWinows.Remove(baseWindow);
            //Destroy(baseWindow.gameObject);
        }
    }
}
