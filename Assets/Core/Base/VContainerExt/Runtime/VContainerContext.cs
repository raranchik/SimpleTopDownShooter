using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Base.VContainerExt.Attributes;
using VContainer;
using VContainer.Unity;

namespace Core.Base.VContainerExt
{
    public class VContainerContext
    {
        private readonly ISet<string> m_Namespaces;
        private readonly ISet<Type> m_FastSearchNamespacesByClasses;
        private readonly Type m_Context;
        private readonly bool m_IsIgnoreNamespaces;
        private readonly IContainerBuilder m_Builder;

        private readonly Type m_LifetimeAttribute = typeof(LifetimeAttribute);
        private readonly Type m_AsAttribute = typeof(AsAttribute);
        private readonly Type m_AsImplementedInterfaces = typeof(AsImplementedInterfacesAttribute);
        private readonly Type m_AsSelf = typeof(AsSelfAttribute);
        private readonly MethodInfo m_RegisterEntryPointMethod;
        private readonly object[] m_RegisterEntryPointMethodArgs;

        public VContainerContext(VContainerContextData data)
        {
            m_Builder = data.Builder;
            m_FastSearchNamespacesByClasses = data.FastSearchNamespacesByClasses;
            m_IsIgnoreNamespaces = data.IsIgnoreNamespaces;
            m_Context = data.Context;
            m_Namespaces = data.Namespaces;
            m_Namespaces?.Add(typeof(VContainerContext).Namespace!);

            var extension = typeof(ContainerBuilderUnityExtensions);
            m_RegisterEntryPointMethod =
                extension.GetMethod(nameof(ContainerBuilderUnityExtensions.RegisterEntryPoint));
            m_RegisterEntryPointMethodArgs = new object[] { m_Builder, null };
        }

        public void InitContext()
        {
            var assemblies = ParseAssemblies();
            var types = ParseTypes(assemblies);
            RegisterTypes(types);
        }

        private void RegisterTypes(IEnumerable<Type> types)
        {
            var plain = typeof(RegisterAttribute);
            var entryPoint = typeof(RegisterEntryPointAttribute);
            foreach (var type in types)
            {
                if (type.IsDefined(plain))
                {
                    RegisterPlainType(type);
                }
                else if (type.IsDefined(entryPoint))
                {
                    RegisterEntryPointType(type);
                }
            }
        }

        private void RegisterPlainType(Type type)
        {
            var lifetime = ParseLifetime(type);
            var registration = m_Builder.Register(type, lifetime);
            if (type.IsDefined(m_AsImplementedInterfaces))
            {
                registration.AsImplementedInterfaces();
            }
            else if (type.IsDefined(m_AsAttribute))
            {
                var synonyms = type.GetCustomAttributes<AsAttribute>().SelectMany(x => x.Types).ToArray();
                registration.As(synonyms);
            }

            if (type.IsDefined(m_AsSelf))
            {
                registration.AsSelf();
            }
        }

        private void RegisterEntryPointType(Type type)
        {
            var lifetime = ParseLifetime(type);
            var method = m_RegisterEntryPointMethod.MakeGenericMethod(type);
            m_RegisterEntryPointMethodArgs[1] = lifetime;
            var registration = method.Invoke(null, m_RegisterEntryPointMethodArgs) as RegistrationBuilder;

            if (type.IsDefined(m_AsAttribute))
            {
                var synonyms = type.GetCustomAttributes<AsAttribute>().SelectMany(x => x.Types).ToArray();
                registration!.As(synonyms);
            }

            if (type.IsDefined(m_AsSelf))
            {
                registration!.AsSelf();
            }
        }

        private Lifetime ParseLifetime(Type type)
        {
            return type.IsDefined(m_LifetimeAttribute)
                ? type.GetCustomAttribute<LifetimeAttribute>().Scope
                : Lifetime.Singleton;
        }

        private IEnumerable<Assembly> ParseAssemblies()
        {
            var assemblies = new List<Assembly>();
            if (m_FastSearchNamespacesByClasses.Count > 0)
            {
                foreach (var type in m_FastSearchNamespacesByClasses)
                {
                    assemblies.Add(type.GetTypeInfo().Assembly);
                }

                assemblies.Add(typeof(VContainerContext).GetTypeInfo().Assembly);
            }
            else
            {
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            }

            return assemblies;
        }

        private IEnumerable<Type> ParseTypes(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types;
            if (!m_IsIgnoreNamespaces)
            {
                types = assemblies.SelectMany(x => x.GetTypes())
                    .Where(x => m_Namespaces.Contains(x.Namespace));
            }
            else
            {
                types = assemblies.SelectMany(x => x.GetTypes());
            }

            var filterByBase = typeof(VContainerBaseAttribute);
            var filterByContext = typeof(ContextAttribute);
            types = types.Where(x => !x.IsAssignableFrom(filterByBase))
                .Where(x => x.IsDefined(filterByBase))
                .Where(x => x.IsDefined(filterByContext))
                .Where(x => x.GetCustomAttribute<ContextAttribute>().Type == m_Context);

            return types;
        }
    }
}