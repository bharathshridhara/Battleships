using System.Collections.Generic;
using System.Threading.Tasks;
using Battleships.Api.Controllers;
using Battleships.Domain.Entities;
using Battleships.Infrastructure.Exceptions;
using Battleships.Services.Dto;
using Battleships.Test.Fixtures;
using Battleships.Test.TestDoubles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Battleships.Test
{
    public class When_Attacked
    {
        public class And_Attack_Falls_On_Ship
        {
            [Theory]
            [InlineData(2, 4, Orientation.Horizontal)]
            [InlineData(3, 4, Orientation.Horizontal)]
            [InlineData(4, 4, Orientation.Horizontal)]
            [InlineData(2, 2, Orientation.Vertical)]
            [InlineData(2, 3, Orientation.Vertical)]
            [InlineData(2, 4, Orientation.Vertical)]
            public async Task It_Returns_Hit_Response(int attackX, int attackY, Orientation orientation)
            {
                //Create a board and ship using the spy
                int boardId = 1;
                var board = new Board
                {
                    Id = boardId,
                    Ships = new List<Ship>
                    {
                        new Ship
                        {
                            Bow = new Coordinates(2, 4),
                            Id = 1,
                            Length = 3,
                            Orientation = orientation
                        }
                    }
                };
                
                var repository = new BattleshipRepositorySpy
                {
                    Boards = new List<Board> { board }
                };
                
                //Create new attack at a location not occupied by ships
                var attack = new AttackDto { AttackX = attackX, AttackY = attackY };
                
                BoardController sut = new BoardControllerFixture()
                    .WithRepository(repository).CreateSut();

                IActionResult response = await sut.Attack(boardId, attack);
                OkObjectResult result = response.Should().BeOfType<OkObjectResult>().Subject;
                AttackOutcome outcome = result.Value.Should().BeAssignableTo<AttackOutcome>().Subject;
                Assert.Equal("Hit", outcome.Outcome);
            }
        }

        public class And_Attack_Fails_To_Find_Ship
        {
            [Theory]
            [InlineData(3, 5)]
            [InlineData(4, 1)]
            [InlineData(1, 1)]
            public async Task It_Returns_Miss_Response(int attackX, int attackY)
            {
                //Create a board and ship using the spy
                int boardId = 1;
                var board = new Board
                {
                    Id = boardId,
                    Ships = new List<Ship>
                    {
                        new Ship
                        {
                            Bow = new Coordinates(2, 4),
                            Id = 1,
                            Length = 3,
                            Orientation = Orientation.Horizontal
                        }
                    }
                };
                
                var repository = new BattleshipRepositorySpy
                {
                    Boards = new List<Board> { board }
                };
                
                //Create new attack at a location not occupied by ships
                var attack = new AttackDto { AttackX = attackX, AttackY = attackY };
                
                BoardController sut = new BoardControllerFixture()
                    .WithRepository(repository).CreateSut();

                IActionResult response = await sut.Attack(boardId, attack);
                OkObjectResult result = response.Should().BeOfType<OkObjectResult>().Subject;
                AttackOutcome outcome = result.Value.Should().BeAssignableTo<AttackOutcome>().Subject;
                Assert.Equal("Miss", outcome.Outcome);
            }
        }

        public class And_Attack_Is_Outside_Board
        {
            [Fact]
            public async Task It_Returns_InvalidAttackException()
            {
                //Create a board using the spy
                int boardId = 1;
                var repository = new BattleshipRepositorySpy
                {
                    Boards = new List<Board>
                    {
                        new Board {Id = boardId}
                    }
                };
                
                var attack = new AttackDto
                {
                    AttackX = 3,
                    AttackY = 12 //Board dimension is 10X10
                };
                
                BoardController sut = new BoardControllerFixture()
                    .WithRepository(repository).CreateSut();

                await Assert.ThrowsAsync<InvalidAttackException>(() => sut.Attack(boardId, attack));
            }
        }

        public class And_Attack_Is_On_Invalid_Board
        {
            [Fact]
            public async Task It_Returns_NotFoundException()
            {
                //Create a board using the spy
                int boardId = 1, wrongBoardId = 2;
                var repository = new BattleshipRepositorySpy
                {
                    Boards = new List<Board>
                    {
                        new Board {Id = boardId}
                    }
                };
                
                var attack = new AttackDto
                {
                     AttackX = 3,
                     AttackY = 4
                };
                
                BoardController sut = new BoardControllerFixture()
                    .WithRepository(repository).CreateSut();

                await Assert.ThrowsAsync<NotFoundException>(() => sut.Attack(wrongBoardId, attack));
            }
        }
    }
}