using UnityEngine;

namespace TacticalModule.Scripts.Managers
{
    public class GameMain : MonoBehaviour
    {
        //private BaseWindow _loader;

        private void Awake()
        {
            //_loader = InterfaceController.OpenWindow(WinowType.Loader);
            //var job = SceneManager.LoadSceneAsync(Scene.Lobby.ToString());
            //job.completed += SceneLoaded;
        }

        private void RegistredCotainers()
        {

        }

        private void SceneLoaded(AsyncOperation obj)
        {
            //InterfaceController.Close(_loader);
            //InterfaceController.OpenWindow(WinowType.Lobby);
        }
    }
}
