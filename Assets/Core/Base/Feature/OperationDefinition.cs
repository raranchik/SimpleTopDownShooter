namespace Core.Base.Feature
{
    public static class OperationDefinition
    {
        public const bool IsSuccessful = true;
        public const bool IsFailed = false;
        public const bool IsAsync = true;
        public const bool IsNotAsync = false;

        public static OperationStatus Successful()
        {
            return new OperationStatus(IsSuccessful, IsNotAsync);
        }

        public static OperationStatus Async()
        {
            return new OperationStatus(IsFailed, IsAsync);
        }
    }
}