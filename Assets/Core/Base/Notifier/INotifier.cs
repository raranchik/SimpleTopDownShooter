namespace Core.Base.Notifier
{
    public interface INotifier<in T>
    {
        void Attach(T observer);
        void Detach(T observer);
    }
}