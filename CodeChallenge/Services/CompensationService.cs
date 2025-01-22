using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    class CompensationService: ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }

        /// <summary>
        /// Retrieves a compensation record for a given employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee whose compensation record is to be retrieved.</param>
        /// <returns>
        /// The <see cref="Compensation"/> object if found, or <c>null</c> if the ID is null, empty, 
        /// or no record exists for the given ID.
        /// </returns>
        public Compensation GetCompensation(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }

        /// <summary>
        /// Creates a new compensation record and saves it to the database.
        /// </summary>
        /// <param name="compensation">The <see cref="Compensation"/> object to be created.</param>
        /// <returns>
        /// The created <see cref="Compensation"/> object.
        /// </returns>
        public Compensation CreateCompensation(Compensation compensation)
        {
            if(compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }
    }
}