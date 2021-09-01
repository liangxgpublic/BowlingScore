using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Web.Http;
using System.Threading.Tasks;
using BowlingScore.Controllers;
using BowlingScore.Models;

namespace BowlingScore_Test
{
    public class ScoresControllerTest
    {
        public ScoresControllerTest()
        {
           
            _controller = new ScoresController();
        }

        private ScoresController _controller;
       
        [Fact]
        // This is Perfect game case
        public async Task Valid_StrikeCompeletedGameTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { 10, 10, 10, 10, 10, 10, 10, 10, 10,10, 10, 10}
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert
            Assert.NotNull(rItem);
            Assert.Equal(200, rItem.StatusCode);
            Assert.IsType<ScoreReponse>(rItem.Value);
            Assert.True((rItem.Value as ScoreReponse).GameCompleted);
            Assert.Collection((rItem.Value as ScoreReponse).FrameProgressScores,
                item => Assert.Equal("30", item),
                item => Assert.Equal("60", item),
                item => Assert.Equal("90", item),
                item => Assert.Equal("120", item),
                item => Assert.Equal("150", item),
                item => Assert.Equal("180", item),
                item => Assert.Equal("210", item),
                item => Assert.Equal("240", item),
                item => Assert.Equal("270", item),
                item => Assert.Equal("300", item)
             );
        }

        [Fact]
        // This is Six frame game case
        public async Task Valid_SixFrameGameTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert
            Assert.NotNull(rItem);
            Assert.Equal(200, rItem.StatusCode);
            Assert.IsType<ScoreReponse>(rItem.Value);
            Assert.True(!(rItem.Value as ScoreReponse).GameCompleted);
            Assert.Collection((rItem.Value as ScoreReponse).FrameProgressScores,
                item => Assert.Equal("2", item),
                item => Assert.Equal("4", item),
                item => Assert.Equal("6", item),
                item => Assert.Equal("8", item),
                item => Assert.Equal("10", item),
                item => Assert.Equal("12", item)               
             );
        }


        [Fact]
        // This is Gut Ball Game case
        public async Task Valid_GutBallGameTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1 }
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert
            Assert.NotNull(rItem);
            Assert.Equal(200, rItem.StatusCode);
            Assert.IsType<ScoreReponse>(rItem.Value);
            Assert.True(!(rItem.Value as ScoreReponse).GameCompleted);
            Assert.Collection((rItem.Value as ScoreReponse).FrameProgressScores,
                item => Assert.Equal("1", item),
                item => Assert.Equal("3", item),
                item => Assert.Equal("4", item),
                item => Assert.Equal("6", item),
                item => Assert.Equal("8", item),
                item => Assert.Equal("10", item)
             );
        }

        [Fact]
        // This case include strike and spare
        public async Task Valid_SevenFrameSpareStrikeGameTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { 1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10 }
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert
            Assert.NotNull(rItem);
            Assert.Equal(200, rItem.StatusCode);
            Assert.IsType<ScoreReponse>(rItem.Value);
            Assert.True(!(rItem.Value as ScoreReponse).GameCompleted);
            Assert.Collection((rItem.Value as ScoreReponse).FrameProgressScores,
                item => Assert.Equal("2", item),
                item => Assert.Equal("4", item),
                item => Assert.Equal("16", item),
                item => Assert.Equal("35", item),
                item => Assert.Equal("55", item),
                item => Assert.Equal("*", item),
                item => Assert.Equal("*", item)
             );
        }

        [Fact]
        // This is error case, Request include minus value
        public async Task InValid_MinusInputTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { 1, 1, 1, 1, -1, 1, 1, 1, 1, 1, 1, 1 }
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert           
            Assert.IsType<BadRequestResult>(TestResult);
           
        }

        [Fact]
        // This is error case, Request is empty
        public async Task InValid_EmptyInputTest()
        {
            // Arrange
            PinDown pItem = new PinDown()
            {
                pinsDowned = { }
            };

            // Act
            var TestResult = await _controller.Scores(pItem);
            var rItem = TestResult as ObjectResult;

            // Assert           
            Assert.IsType<BadRequestResult>(TestResult);

        }
    }
}
