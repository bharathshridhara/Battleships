using System;
using Battleships.Api.Controllers;
using Battleships.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Battleships.Test.Fixtures
{
    public class BoardControllerFixture
    {
        private Action<IServiceCollection> _setup;

        public BoardControllerFixture()
        {
            _setup = s => { };
        }
        public BoardController CreateSut()
        {
            var provider = TestServiceProvider.CreateProvider(_setup);
            return provider.GetRequiredService<BoardController>();
        }

        public BoardControllerFixture WithRepository(IBattleshipsRepository repository)
        {
            _setup += s => s.Replace(ServiceDescriptor.Transient(provider => repository));
            return this;
        }
    }
}