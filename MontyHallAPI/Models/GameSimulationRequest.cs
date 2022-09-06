namespace MontyHallAPI.Models
{
    public class GameSimulationRequest
    {

        public int NumberOfSimulations { get; set; }
        public bool IsSwitching { get; set; }
        public int NumberOfDoors { get; } = 3;

        public Application.Models.GameSimulationRequest ToDomain()
        {
            return new Application.Models.GameSimulationRequest
            {
                IsSwitching = this.IsSwitching,
                NumberOfSimulations = this.NumberOfSimulations
            };
        }

    }
}