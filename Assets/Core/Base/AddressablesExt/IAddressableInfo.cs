namespace Core.Base.AddressablesExt
{
    public interface IAddressableInfo
    {
        public string Key { get; }
        public bool IsLoaded { get; }
        bool IsPreloaded { get; }
        float UsedLastTime { get; }
        public bool MarkToUnload { get; }
        public bool IsFailed { get; }
    }
}