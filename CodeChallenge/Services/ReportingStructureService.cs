using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;


namespace CodeChallenge.Services
{
    public class ReportingStructureService: IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// A recursive function that calculates the total number of people reporting to the provided employee.
        /// </summary>
        /// <param name="id">The ID of the employee whose reporters are being calculated.</param>
        /// <returns>The total number of employees that report to the provided employee.</returns>
        public int CalculateTotalReports(string id)
        {
            Employee employee = _employeeService.GetById(id);

            int totalReports = 0;
            if (employee != null && employee.DirectReports != null)
            {
                employee.DirectReports.ForEach(report => {
                    totalReports++;
                    totalReports += CalculateTotalReports(report.EmployeeId);
                });
            }
            return totalReports;
        }
    }
}