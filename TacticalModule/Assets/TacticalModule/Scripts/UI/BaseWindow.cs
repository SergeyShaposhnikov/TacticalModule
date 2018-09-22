using TacticalModule.Scripts.Controller;
using UnityEngine;

namespace TacticalModule.Scripts.UI
{
    public class BaseWindow : MonoBehaviour
    {
        public WinowType Type { get; private set; }

        public virtual void InitWindow(WinowType type)
        {
            Type = type;
        }

        public virtual void Close()
        {
            InterfaceController.Close(this);
        }
    }
}