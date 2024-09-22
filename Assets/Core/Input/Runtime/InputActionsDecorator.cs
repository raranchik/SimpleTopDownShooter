using System;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using VContainer.Unity;

namespace Core.Input
{
    [RegisterEntryPoint, As(typeof(InputActions)), Context(typeof(StartContext))]
    public class InputActionsDecorator : InputActions, IInitializable, IDisposable
    {
        public void Initialize()
        {
            Enable();
        }

        public new void Dispose()
        {
            Disable();
            base.Dispose();
        }
    }
}