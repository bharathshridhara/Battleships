using System.Collections.Generic;
using System.Threading.Tasks;
using Battleships.Domain.Entities;
using Battleships.Infrastructure.Exceptions;
using Battleships.Services.Dto;
using Battleships.Test.Builders;
using Battleships.Test.Fixtures;
using Battleships.Test.TestDoubles;
using Battleships.Test.Theories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Battleships.Test
{
    public class When_Creating_New_Ship
    {
        [Fact]
        public async Task It_Creates_New_Ship()
        {
            //Create a board using the spy
            var boardId = 1;
            var repository = new BattleshipRepositorySpy
            {
                Boards = new List<Board>
                {
                    new Board {Id = boardId}
                }
            };

            var ship = new ShipBuilder()
                .WithLength(2)
                .WithOrientation(Orientation.Horizontal)
                .WithBowX(1)
                .WithBowY(1)
                .Build();

            var sut = new ShipControllerFixture()
                .WithRepository(repository)
                .CreateSut();

            var response = await sut.Create(boardId, ship);
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var shipResponse = okResult.Value.Should().BeAssignableTo<ShipDto>().Subject;
            Assert.Equal(ship.Length, shipResponse.Length);
            Assert.Equal(ship.BowX, shipResponse.BowX);
            Assert.Equal(ship.BowY, shipResponse.BowY);
        }

        public class And_Board_Is_Invalid
        {
            [Fact]
            public async Task It_Returns_NotFoundException()
            {
                var sut = new ShipControllerFixture().CreateSut();

                //Create invalid boardId
                var boardId = 9;
                var shipDto = new ShipBuilder().Build();
                await Assert.ThrowsAsync<NotFoundException>(() => sut.Create(boardId, shipDto));
            }
        }

        public class And_Ship_Location_Is_Outside_Board
        {
            [Theory]
            [ClassData(typeof(ShipCreationTheories))]
            public async Task It_Throws_InvalidShipPositionException(string description, ShipCreationTheoryData theory)
            {
                //Create a board using the spy
                var repository = new BattleshipRepositorySpy
                {
                    Boards = new List<Board>
                    {
                        theory.Board
                    }
                };

                var sut = new ShipControllerFixture()
                    .WithRepository(repository)
                    .CreateSut();

                await Assert.ThrowsAsync<InvalidShipPositionException>(
                    () => sut.Create(theory.Board.Id, theory.ShipDto));
            }
        }
    }
}