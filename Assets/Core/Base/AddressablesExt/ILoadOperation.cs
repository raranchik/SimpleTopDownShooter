using Cysharp.Threading.Tasks;

namespace Core.Base.AddressablesExt
{
    public interface ILoadOperation
    {
        IAddressableInfo AddressableInfo { get; }
        public bool IsLoaded { get; }
        public bool IsFailed { get; }
        UniTaskVoid Execute(AddressablesService addressablesService);
    }
}