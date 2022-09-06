using System;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Models;

namespace MontyHallAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : Controller
    {
        private readonly ISimulationService _simulationService;
        public SimulationController(ISimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] GameSimulationRequest request)
        {
            try
            {
                //Is there a limited number of simulations?
                if (request.NumberOfSimulations <= 0) return BadRequest("Number of simulations cannot be less or equal to zero");

                var simulationResult = _simulationService.RunSimulation(request.ToDomain());
                return Ok(GameSimulationResponse.FromDomain(simulationResult));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }


        }
    }
}