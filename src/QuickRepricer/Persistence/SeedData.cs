using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickRepricer.Core;
using QuickRepricer.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRepricer.Persistence
{
    public class SeedData
    {
        private IConfigurationRoot _config;
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public SeedData(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            IConfigurationRoot config, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _unitOfWork = unitOfWork;
        }


        public async Task EnsureSeedData()
        {
            var plans = new List<Plan>()
            {
                new Plan() { Id = 1, ExternalId = "basic-monthly", Description = "Basic Monthly Plan", DisplayOrder = 1, IsActive = true, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin" },
                new Plan() { Id = 2, ExternalId = "premium-monthly", Description = "Premium Monthly Plan", DisplayOrder = 2, IsActive = true, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin" },
                //new Plan() { Id = 3, ExternalId = "", Description = "Custom Plan", DisplayOrder = 3, IsActive = true, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"}
            };

            foreach (var plan in plans)
            {
                if (await _unitOfWork.Plans.GetPlanByIdAsync(plan.Id) == null)
                {
                    _unitOfWork.Plans.Add(plan);
                }
            }
            
            if (!await _unitOfWork.Features.AnyFeatureAsync())
            {
                var features = new List<Feature>()
                {
                    new Feature() { PlanId = 1, Description = "Includes 10k Listings", DisplayOrder = 1, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "1 Marketplace", DisplayOrder = 2, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "Continuous Repricing", DisplayOrder = 3, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "Continuous Support", DisplayOrder = 4, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "Pre-Configured Strategies", DisplayOrder = 5, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "Advanced Custom Strategies", DisplayOrder = 6, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 1, Description = "Algorithmic Strategies", DisplayOrder = 7, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},

                    new Feature() { PlanId = 2, Description = "Up to 50k Listings", DisplayOrder = 1, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "1 of Each Marketplace", DisplayOrder = 2, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "Continuous Repricing", DisplayOrder = 3, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "Continuous Support", DisplayOrder = 4, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "Pre-Configured Strategies", DisplayOrder = 5, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "Advanced Custom Strategies", DisplayOrder = 6, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"},
                    new Feature() { PlanId = 2, Description = "Algorithmic Strategies", DisplayOrder = 7, CreatedAt = DateTime.Now, CreatedBy = "admin", ModifiedAt = DateTime.Now, ModifiedBy = "admin"}
                };

                _unitOfWork.Features.AddRange(features);
            }
            
            await _context.Database.OpenConnectionAsync();
            await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Plans ON");

            await _unitOfWork.CompleteAsync();

            await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Plans OFF");
            _context.Database.CloseConnection();
        }
    }
}
