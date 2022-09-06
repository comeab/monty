using Application.Models;
using System;
using System.Threading.Tasks;

namespace Application.Services
{

    public class SimulationService : ISimulationService
    {
        public GameSimulationResult RunSimulation(GameSimulationRequest request)
        {
            int wins = 0;
            int degreeParalism = Environment.ProcessorCount;

            Parallel.For(0, degreeParalism,
                  workerId =>
                  {
                      var max = request.NumberOfSimulations * (workerId + 1) / degreeParalism;

                      for (int i = request.NumberOfSimulations * workerId / degreeParalism; i < max; i++)
                      {
                          GameRun gameRun = new GameRun();
                          var hasWon = gameRun.HasUserWon(request.IsSwitching);
                          if (hasWon) wins++;
                      }
                  });

            return new GameSimulationResult()
            {
                Wins = wins,
                PercentageOfWins = ((decimal)wins / request.NumberOfSimulations)
            };
        }
    }

}