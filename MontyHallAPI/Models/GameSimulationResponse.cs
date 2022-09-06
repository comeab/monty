
using Application.Models;
using System;

namespace MontyHallAPI.Models
{
    public class GameSimulationResponse
    {
        public decimal Wins{get;set;}
        public decimal PercentageOfWins{get;set;}

        public static GameSimulationResponse FromDomain(GameSimulationResult result)
        {
            return new GameSimulationResponse
            {
                PercentageOfWins = result.PercentageOfWins,
                Wins = result.Wins
            };
        }

    }
}