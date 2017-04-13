using FluentAssertions;
using Moq;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription.Plans;
using System.Collections.Generic;
using Xunit;

namespace QuickRepricer.Web.Tests.Services.Subscription.Plans
{
    public class PlanServiceTest
    {

        private Mock<IPlanService> _planservice;

        public PlanServiceTest()
        {
            _planservice = new Mock<IPlanService>();
            
        }


        //     Task<Plan> FindAsync(int id);
        [Theory]
        [InlineData(1)]
        public void FindAsync_RetunsAPlan(int id)
        {
            //wireup
            _planservice.Setup(stub => stub.FindAsync(id)).ReturnsAsync(new Plan() { Id = id });

            var plan = _planservice.Object.FindAsync(id);

            plan.Should().NotBeNull();

        }

        [Fact]
        public void FindAsync_RetunsPlans()
        {
            //wireup
            _planservice.Setup(stub => stub.ListAsync()).ReturnsAsync(new List<Plan> { new Plan() });

            var plans = _planservice.Object.ListAsync();

            plans.Should().NotBeNull();

        }
    }
}
