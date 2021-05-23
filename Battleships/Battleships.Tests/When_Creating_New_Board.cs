using System.Threading.Tasks;
using Battleships.Services.Dto;
using Battleships.Test.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Battleships.Test
{
    public class When_Creating_New_Board
    {
        [Fact]
        public async Task It_Returns_A_New_Board()
        {
            var sut = new BoardControllerFixture().CreateSut();
            var boardResponse = await sut.Create();
            var result = boardResponse.Should().BeOfType<OkObjectResult>().Subject;
            var board = result.Value.Should().BeAssignableTo<BoardDto>().Subject;

            Assert.Equal(10, board.Length);
            Assert.Equal(10, board.Breadth);
            Assert.True(board.Id > 0);
        }
    }
}