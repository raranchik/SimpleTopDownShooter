using System.Collections.Generic;
using Core.Base.VContainerExt.Attributes;
using VContainer;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1))]
    public class ContextT1Service
    {
        [Inject] public readonly ComboT1 ComboT1;
        [Inject] public readonly ComboT2 ComboT2;
        [Inject] public readonly ComboT3 ComboT3;
        [Inject] public readonly ComboT4 ComboT4;
        [Inject] public readonly EntryPointT1 EntryPointT1;
        [Inject] public readonly EntryPointT2 EntryPointT2;
        [Inject] public readonly PlainAsImplementedInterfacesT1 PlainAsImplementedInterfacesT1;
        [Inject] public readonly PlainAsImplementedInterfacesT2 PlainAsImplementedInterfacesT2;
        [Inject] public readonly PlainAsImplementedInterfacesT3 PlainAsImplementedInterfacesT3;
        [Inject] public readonly PlainAsSelfT1 PlainAsSelfT1;
        [Inject] public readonly PlainAsSelfT2 PlainAsSelfT2;
        [Inject] public readonly PlainAsT1 PlainAsT1;
        [Inject] public readonly PlainAsT2 PlainAsT2;
        [Inject] public readonly PlainAsT3 PlainAsT3;
        [Inject] public readonly PlainAsT4 PlainAsT4;
        [Inject] public readonly PlainT1 PlainT1;
        [Inject] public readonly PlainT2 PlainT2;
        [Inject] public IReadOnlyList<IAsT1> m_AsT1;
        [Inject] public IReadOnlyList<IAsT1> m_AsT2;
    }
}