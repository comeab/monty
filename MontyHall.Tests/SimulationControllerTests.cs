
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Controllers;
using MontyHallAPI.Models;
using Moq;
using NUnit.Framework;
using System;

namespace MontyHall.Tests
{
    [TestFixture]
    public class SimulationControllerTests
    {

        [TestCase(100, 60)]
        [TestCase(100, 30)]
        [TestCase(1000000, 910000)]
        public void Post_Game_ReturnsSimulationResults(int NumberOfSimulations, int nrWins)
        {

            var simulationResultObj = new Application.Models.GameSimulationResult()
            {
                Wins = nrWins,
                PercentageOfWins = nrWins / NumberOfSimulations,
            };
            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(service => service.RunSimulation(It.IsAny<Application.Models.GameSimulationRequest>())).Returns(simulationResultObj);
            var sut = new SimulationController(mockSimulationService.Object);

            var result = sut.Post(new GameSimulationRequest() { NumberOfSimulations = NumberOfSimulations }) as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);

            var simulationResult = result.Value as GameSimulationResponse;
            Assert.GreaterOrEqual(simulationResult.Wins, nrWins);
            Assert.GreaterOrEqual(simulationResult.PercentageOfWins, simulationResultObj.PercentageOfWins);
        }


        [Test]
        public void Post_NumberOfSimulationsIsZero_Returns400()
        {
            var mockSimulationService = new Mock<ISimulationService>();

            var sut = new SimulationController(mockSimulationService.Object);
            var result = sut.Post(new GameSimulationRequest() { NumberOfSimulations = 0 }) as BadRequestObjectResult;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var simulationResult = result.Value as GameSimulationResponse;

            Assert.IsNull(simulationResult);
        }

        [Test]
        public void Post_throwsException_Returns500()
        {

            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(repo => repo.RunSimulation(It.IsAny<Application.Models.GameSimulationRequest>())).Throws(new Exception("Random exception"));
            var sut = new SimulationController(mockSimulationService.Object);

            var result = sut.Post(new GameSimulationRequest() { NumberOfSimulations = 10 }) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 500);

            var simulationResult = result.Value as GameSimulationResponse;
            Assert.IsNull(simulationResult);
        }
    }
}