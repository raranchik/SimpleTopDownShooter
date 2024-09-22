namespace Core.Base
{
    public readonly struct Result<T>
    {
        public bool HasValue { get; }
        public T Value { get; }

        public Result(bool hasValue, T value)
        {
            HasValue = hasValue;
            Value = value;
        }

        public bool IsValid()
        {
            return HasValue;
        }

        public static Result<T> Invalid()
        {
            return new Result<T>(false, default(T));
        }

        public static Result<T> Successful(T value)
        {
            return new Result<T>(true, value);
        }
    }
}