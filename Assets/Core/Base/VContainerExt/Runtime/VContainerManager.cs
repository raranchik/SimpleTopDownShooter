using System;
using System.Collections.Generic;

namespace Core.Base.VContainerExt
{
    public class VContainerManager : IVContainerManager
    {
        private readonly Dictionary<Type, VContainerContext> m_Contexts = new Dictionary<Type, VContainerContext>();

        public void InitializeContext(VContainerContextData contextData)
        {
            if (m_Contexts.ContainsKey(contextData.Context))
            {
                return;
            }

            var context = new VContainerContext(contextData);
            context.Initialize();
            m_Contexts.Add(contextData.Context, context);
        }

        public void DestroyContext(Type contextType)
        {
            if (!m_Contexts.TryGetValue(contextType, out var context))
            {
                return;
            }

            context.Dispose();
            m_Contexts.Remove(contextType);
        }
    }
}