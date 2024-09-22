using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core.Base.VContainerExt
{
    public class VContainerContext : IDisposable
    {
        private readonly ISet<string> m_Namespaces;
        private readonly ISet<Type> m_FastSearchNamespacesByClasses;
        private readonly Type m_Context;
        private readonly bool m_IsIgnoreNamespaces;
        private readonly IContainerBuilder m_Builder;
        private readonly LifetimeScope m_Scope;

        private readonly Type m_LifetimeAttribute = typeof(LifetimeAttribute);
        private readonly Type m_AsAttribute = typeof(AsAttribute);
        private readonly Type m_AsImplementedInterfaces = typeof(AsImplementedInterfacesAttribute);
        private readonly Type m_AsSelf = typeof(AsSelfAttribute);
        private readonly MethodInfo m_RegisterEntryPointMethod;
        private readonly object[] m_RegisterEntryPointMethodArgs = new object[2];
        private readonly MethodInfo m_RegisterInstanceMethod;
        private readonly object[] m_RegisterInstanceMethodArgs = new object[2];

        public VContainerContext(VContainerContextData data)
        {
            m_Builder = data.Builder;
            m_FastSearchNamespacesByClasses = data.FastSearchNamespacesByClasses;
            m_IsIgnoreNamespaces = data.IsIgnoreNamespaces;
            m_Context = data.Context;
            m_Namespaces = data.Namespaces;
            m_Namespaces?.Add(typeof(VContainerContext).Namespace!);
            m_Scope = data.Scope;

            var extension = typeof(ContainerBuilderUnityExtensions);
            var methodName = nameof(ContainerBuilderUnityExtensions.RegisterEntryPoint);
            m_RegisterEntryPointMethod = extension.GetMethod(methodName);
            m_RegisterEntryPointMethodArgs[0] = m_Builder;
            extension = typeof(ContainerBuilderExtensions);
            methodName = nameof(ContainerBuilderExtensions.RegisterInstance);
            m_RegisterInstanceMethod = extension.GetMethods()
                .First(x => x.Name == methodName && x.ContainsGenericParameters && x.GetGenericArguments().Length == 1);
            m_RegisterInstanceMethodArgs[0] = m_Builder;
        }

        public void Initialize()
        {
            var assemblies = ParseAssemblies();
            var types = ParseTypes(assemblies);
            RegisterTypes(types);
        }

        public void Dispose()
        {
            m_Scope.Dispose();
        }

        private void RegisterTypes(IEnumerable<Type> types)
        {
            var plain = typeof(RegisterAttribute);
            var entryPoint = typeof(RegisterEntryPointAttribute);
            var monoInHierarchy = typeof(MonoInHierarchyAttribute);
            var configurator = typeof(ConfiguratorAttribute);
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
                else if (type.IsDefined(monoInHierarchy))
                {
                    RegisterMonoInHierarchy(type);
                }
                else if (type.IsDefined(configurator))
                {
                    HandleConfigurator(type);
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
                var synonyms = type.GetCustomAttributes<AsAttribute>()
                    .SelectMany(x => x.Types)
                    .ToArray();
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
            if (registration == null)
            {
                Debug.LogError($"Registration is null");
                return;
            }

            if (type.IsDefined(m_AsAttribute))
            {
                var synonyms = type.GetCustomAttributes<AsAttribute>()
                    .SelectMany(x => x.Types)
                    .ToArray();
                registration.As(synonyms);
            }

            if (type.IsDefined(m_AsSelf))
            {
                registration.AsSelf();
            }
        }

        private void RegisterMonoInHierarchy(Type type)
        {
            var component = default(Component);
            using (ListPool<GameObject>.Get(out var gameObjectBuffer))
            {
                var scene = SceneManager.GetActiveScene();
                scene.GetRootGameObjects(gameObjectBuffer);
                foreach (var gameObject in gameObjectBuffer)
                {
                    component = gameObject.GetComponentInChildren(type, true);
                    if (component)
                    {
                        break;
                    }
                }
            }

            if (!component)
            {
                Debug.LogError($"Component not found: type<{type.AssemblyQualifiedName}>");
                return;
            }

            var registration = m_Builder.RegisterComponent(component);
            if (type.IsDefined(m_AsImplementedInterfaces))
            {
                registration.AsImplementedInterfaces();
            }
            else if (type.IsDefined(m_AsAttribute))
            {
                var synonyms = type.GetCustomAttributes<AsAttribute>()
                    .SelectMany(x => x.Types)
                    .ToArray();
                registration.As(synonyms);
            }

            if (type.IsDefined(m_AsSelf))
            {
                registration.AsSelf();
            }
        }

        private void HandleConfigurator(Type type)
        {
            var configurator = Activator.CreateInstance(type);
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
            foreach (var beanMethod in type.GetMethods(bindingFlags))
            {
                var returnParameter = beanMethod.ReturnParameter;
                if (returnParameter == null || returnParameter.ParameterType == typeof(void))
                {
                    continue;
                }

                foreach (var attribute in Attribute.GetCustomAttributes(beanMethod))
                {
                    if (attribute is not BeanAttribute)
                    {
                        continue;
                    }

                    var registerType = returnParameter.ParameterType;
                    var instance = beanMethod.Invoke(configurator, Array.Empty<object>());
                    var registerMethod = m_RegisterInstanceMethod.MakeGenericMethod(registerType);
                    m_RegisterInstanceMethodArgs[1] = instance;
                    registerMethod.Invoke(null, m_RegisterInstanceMethodArgs);
                }
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