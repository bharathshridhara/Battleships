using System;
using Battleships.Api.Controllers;
using Battleships.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Battleships.Test.Fixtures
{
    public class ShipControllerFixture
    {
        private Action<IServiceCollection> _setup;

        public ShipControllerFixture()
        {
            _setup = services => { };
            ;
        }

        public ShipController CreateSut()
        {
            var provider = TestServiceProvider.CreateProvider(_setup);
            return provider.GetRequiredService<ShipController>();
        }

        public ShipControllerFixture WithRepository(IBattleshipsRepository repository)
        {
            _setup += s => s.Replace(ServiceDescriptor.Transient(provider => repository));
            return this;
        }
    }
}