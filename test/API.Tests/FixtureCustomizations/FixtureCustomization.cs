using AutoFixture;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DataAccess.Models;

namespace API.Tests.FixtureCustomizations
{
    public class FixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<IDistrict>(c => c.FromFactory(() => fixture.Create<District>()));
            fixture.Customize<IDistrictDetails>(c => c.FromFactory(() => fixture.Create<DistrictDetails>()));
            fixture.Customize<IStore>(c => c.FromFactory(() => fixture.Create<Store>()));
            fixture.Customize<ISalesperson>(c => c.FromFactory(() => fixture.Create<Salesperson>()));
        }
    }
}
