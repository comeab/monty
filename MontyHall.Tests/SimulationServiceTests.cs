using Application.Services;
using NUnit.Framework;

namespace MontyHall.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SimulationServiceTests
    {
        private SimulationService _simulationService;

        [SetUp]
        public void SetUp()
        {
            _simulationService = new SimulationService();
        }

        [TestCase(20, 0.5)]
        [TestCase(500)]
        [TestCase(1_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        //[TestCase(int.MaxValue)]
        public void RunSimulation_IsSwitchingFalse_ReturnsLessOrEqualTo40Percent(int numberOfSimulations, decimal expectedUpperBound = 0.4M)
        {
            var simulationResult = _simulationService.RunSimulation(new Application.Models.GameSimulationRequest
            {
                NumberOfSimulations = numberOfSimulations,
                IsSwitching = false
            });
            //Upper bound is 40%
            Assert.LessOrEqual(simulationResult.PercentageOfWins, expectedUpperBound);
        }

        [TestCase(20, 0.5)]
        [TestCase(500)]
        [TestCase(1_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        //[TestCase(int.MaxValue)]
        public void RunSimulation_IsSwitchingTrue_ReturnsMoreOrEqualTo60Percent(int numberOfSimulations, decimal expectedLowerBound = 0.6M)
        {
            var simulationResult = _simulationService.RunSimulation(new Application.Models.GameSimulationRequest { 
                NumberOfSimulations = numberOfSimulations,
                IsSwitching = true
            });
            //Lower bound is 60%
            Assert.GreaterOrEqual(simulationResult.PercentageOfWins, expectedLowerBound);
        }
    }
}