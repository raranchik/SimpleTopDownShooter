namespace Core.Base.Factory
{
    public interface IFactoryWithArgs<out TCreate, in TArgs>
    {
        TCreate Create(TArgs args);
    }
}