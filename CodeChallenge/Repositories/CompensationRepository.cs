
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using System.Runtime.CompilerServices;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;

        public CompensationRepository(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public Compensation GetByEmployeeId(string id)
        {
            return _compensationContext.Compensations
                .Where(c => c.Employee == id)
                .FirstOrDefault();
        
        }

        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}