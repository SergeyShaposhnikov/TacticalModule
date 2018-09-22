using TacticalModule.Scripts.Managers;

namespace TacticalModule.Scripts.Provider
{
    public interface IProvider
    {
        void Register(IController newManager);
        void Unregister(IController destroyManager);
    }
}
