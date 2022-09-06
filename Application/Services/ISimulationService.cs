using Application.Models;

namespace Application.Services
{
    public interface ISimulationService
    {
        public GameSimulationResult RunSimulation(GameSimulationRequest request);
    }

}