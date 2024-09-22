namespace Core.Base.Feature
{
    public struct OperationStatus
    {
        public bool IsSuccessful;
        public bool IsAsync;

        public OperationStatus(bool isSuccessful, bool isAsync)
        {
            IsSuccessful = isSuccessful;
            IsAsync = isAsync;
        }
    }
}