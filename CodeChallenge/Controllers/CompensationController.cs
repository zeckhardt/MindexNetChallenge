
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// Creates a new compensation record for an employee.
        /// </summary>
        /// <param name="compensation">The <see cref="Compensation"/> object containing the details of the compensation to be created.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> with the created compensation record and a location header referencing the newly created resource.
        /// </returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for {compensation}");        

            _compensationService.CreateCompensation(compensation);

            return CreatedAtRoute(
                routeName: "getCompensation", 
                routeValues: new { id = compensation.Employee }, 
                value: compensation
            );
        }

        /// <summary>
        /// Retrieves the compensation details for an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee whose compensation details are to be retrieved.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the compensation details if found, 
        /// or a <see cref="NotFoundResult"/> if the employee does not exist.
        /// </returns>
        [HttpGet("{id}", Name = "getCompensation")]
        public IActionResult GetCompensation(string id)
        {
            _logger.LogDebug($"Received compensation read request for id {id}");

            var compensation = _compensationService.GetCompensation(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }

}