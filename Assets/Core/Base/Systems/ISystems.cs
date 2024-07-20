namespace Core.Base.Systems
{
    public interface ISystems
    {
        void Attach(ISystem system);
        void Detach(ISystem system);
        void Run();
    }
}