namespace Core.Base.Feature
{
    public interface IFeature
    {
        OperationStatus PreInitialize();
        OperationStatus Initialize();
        OperationStatus PostInitialize();
    }
}