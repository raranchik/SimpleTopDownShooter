using System;
using System.Collections.Generic;

namespace Core.Base.Feature
{
    public interface ISystem
    {
        Type GetName();
        void AddFeature(int order, IFeature feature);
        IEnumerable<IFeature> GetFeatures();
        bool IsPredicatesCompleted();
        void OnInitializeCompleted();
        void SortFeatures();
    }
}