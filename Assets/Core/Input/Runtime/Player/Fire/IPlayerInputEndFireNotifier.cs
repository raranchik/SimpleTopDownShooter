using Core.Base.Notifier;

namespace Core.Input.Player.Fire
{
    public interface IPlayerInputEndFireNotifier : INotifier<IPlayerInputEndFireObserver>
    {
        void OnEndFire();
    }
}