using System.Collections.Generic;

namespace TacticalModule.Scripts.Model
{
    public class TacticalGame
    {
        public Location CurrentLocation;
        public int stepState = 1;
        public List<LocationObjects> _locationObjects = new List<LocationObjects>();
        public List<Team> Teams;
    }
}
