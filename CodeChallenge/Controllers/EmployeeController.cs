using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }


        /// <summary>
        /// Creates a new employee record.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object containing the details of the employee to be created.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the created employee's details, 
        /// along with a location header referencing the newly created resource.
        /// </returns>
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        /// <summary>
        /// Retrieves the details of an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee whose details are to be retrieved.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the employee details if found, 
        /// or a <see cref="NotFoundResult"/> if no employee with the given ID exists.
        /// </returns>
        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Replaces an existing employee's details with new information.
        /// </summary>
        /// <param name="id">The ID of the employee to be updated.</param>
        /// <param name="newEmployee">The <see cref="Employee"/> object containing the new details for the employee.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the updated employee details if successful, 
        /// or a <see cref="NotFoundResult"/> if the employee with the given ID does not exist.
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
    }
}
