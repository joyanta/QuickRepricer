using QuickRepricer.Persistence;
using QuickRepricer.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickRepricer.Core.Repositories;

namespace QuickRepricer.Persistence.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private ApplicationDbContext _context;

        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Feature feature)
        {
            _context.Add(feature);
        }

        public void AddRange(List<Feature> features)
        {
            _context.AddRange(features);
        }

        public async Task<bool> AnyFeatureAsync()
        {
            return await _context.Features.AnyAsync();
        }
    }
}
