using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employee record and saves it to the repository.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object containing the details of the employee to be created.</param>
        /// <returns>
        /// The <see cref="Employee"/> object that was created and saved to the repository, 
        /// or <c>null</c> if the provided employee is <c>null</c>.
        /// </returns>
        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        /// <summary>
        /// Retrieves an employee record by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be retrieved.</param>
        /// <returns>
        /// The <see cref="Employee"/> object if found, or <c>null</c> if the ID is invalid (null or empty) 
        /// or no record exists for the given ID.
        /// </returns>
        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        /// <summary>
        /// Replaces an existing employee record with a new one.
        /// </summary>
        /// <param name="originalEmployee">The <see cref="Employee"/> object to be replaced.</param>
        /// <param name="newEmployee">The <see cref="Employee"/> object containing the new employee details.</param>
        /// <returns>
        /// The <see cref="Employee"/> object representing the new employee with the ID of the original employee.
        /// If <paramref name="originalEmployee"/> is <c>null</c>, no action is performed, and <c>null</c> is returned.
        /// </returns>
        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
    }
}
