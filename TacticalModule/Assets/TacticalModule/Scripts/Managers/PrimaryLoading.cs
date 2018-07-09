using TacticalModule.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TacticalModule.Scripts.Managers
{
    public class PrimaryLoading : MonoBehaviour
    {
        private BaseWindow _loader;

        private void Awake()
        {
            _loader = InterfaceController.OpenWindow(WinowType.Loader);
            var job = SceneManager.LoadSceneAsync(Scene.Lobby.ToString());
            job.completed += SceneLoaded;
        }

        private void SceneLoaded(AsyncOperation obj)
        {
            InterfaceController.Close(_loader);
            InterfaceController.OpenWindow(WinowType.Lobby);
        }
    }
}
