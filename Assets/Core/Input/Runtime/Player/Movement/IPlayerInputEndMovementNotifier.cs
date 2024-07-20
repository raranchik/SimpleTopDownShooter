using Core.Base.Notifier;

namespace Core.Input.Player.Movement
{
    public interface IPlayerInputEndMovementNotifier : INotifier<IPlayerInputEndMovementObserver>
    {
        void OnEndMove();
    }
}