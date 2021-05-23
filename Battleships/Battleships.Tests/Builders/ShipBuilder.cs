using System;
using Battleships.Domain.Entities;
using Battleships.Services.Dto;

namespace Battleships.Test.Builders
{
    public class ShipBuilder
    {
        private Action<ShipDto> _setup;

        public ShipBuilder()
        {
            _setup = s => { };
            WithLength(RandomBuilder.NextInt());
            WithOrientation(RandomBuilder.NextEnum<Orientation>());
            WithBowX(RandomBuilder.NextInt());
            WithBowY(RandomBuilder.NextInt());
        }

        public ShipDto Build()
        {
            var ship = new ShipDto();
            _setup(ship);

            return ship;
        }

        public ShipBuilder WithBowX(int bowX)
        {
            _setup += s => s.BowX = bowX;
            return this;
        }

        public ShipBuilder WithBowY(int bowY)
        {
            _setup += s => s.BowY = bowY;
            return this;
        }

        public ShipBuilder WithOrientation(Orientation orientation)
        {
            _setup += s => s.Orientation = orientation;
            return this;
        }

        public ShipBuilder WithLength(int length)
        {
            _setup += s => s.Length = length;
            return this;
        }
    }
}