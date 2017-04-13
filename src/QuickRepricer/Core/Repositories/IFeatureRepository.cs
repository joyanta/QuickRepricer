using QuickRepricer.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Repositories
{
    public interface IFeatureRepository
    {
        void Add(Feature feature);

        void AddRange(List<Feature> features);

        Task<bool> AnyFeatureAsync();
    }
}