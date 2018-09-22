using UnityEngine;

namespace TacticalModule.Scripts.Model
{
    public class Location
    {
        public string SceneName { get; private set; }
        public Vector2Int Size { get; private set; }

        public Location(string sceneName, Vector2Int size)
        {
            SceneName = sceneName;
            Size = size;
        }
    }
}