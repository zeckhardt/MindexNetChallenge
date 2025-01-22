using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService,
        IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _reportingStructureService = reportingStructureService;
        }

        /// <summary>
        /// Retrieves the reporting structure for an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee whose reporting structure is to be retrieved.</param>
        /// <returns>A <see cref="ReportingStructure"/> object containing the employee details and total reports.</returns>
        [HttpGet("{id}")]
        public ActionResult GetReportingStructure(string id)
        {
            Employee employee = _employeeService.GetById(id);
            int totalReports = _reportingStructureService.CalculateTotalReports(id);

            ReportingStructure reportingStructure = new()
            {
                Employee = employee,
                NumberOfReports = totalReports
            };

            return Ok(reportingStructure);
        }
    }
}