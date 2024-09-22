using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Base.Feature
{
    public class SimpleLauncher : ILauncher
    {
        public IEnumerator InitializeSystems(IEnumerable<ISystem> systems)
        {
            var listSystems = systems.ToList();
            PreInitializeSystems(listSystems);

            for (var i = 0; i < listSystems.Count; ++i)
            {
                var system = listSystems[i];
                if (system.IsPredicatesCompleted())
                {
                    yield return InitializeFeatures(system.GetFeatures());
                    system.OnInitializeCompleted();
                    continue;
                }

                i--;
                yield return null;
            }

            PostInitializeSystems(listSystems);
            yield break;
        }

        private IEnumerator InitializeFeatures(IEnumerable<IFeature> features)
        {
            var listFeatures = features.ToList();
            for (var i = 0; i < listFeatures.Count; ++i)
            {
                var feature = listFeatures[i];
                var status = feature.Initialize();
                if (status.IsAsync)
                {
                    i--;
                }

                yield return null;
            }
        }

        private void PreInitializeSystems(IEnumerable<ISystem> systems)
        {
            foreach (var system in systems)
            {
                foreach (var feature in system.GetFeatures())
                {
                    feature.PreInitialize();
                }
            }
        }

        private void PostInitializeSystems(IEnumerable<ISystem> systems)
        {
            foreach (var system in systems)
            {
                foreach (var feature in system.GetFeatures())
                {
                    feature.PostInitialize();
                }
            }
        }
    }
}