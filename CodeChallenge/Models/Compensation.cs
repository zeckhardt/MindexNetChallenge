using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Key]
        public String CompensationId { get; set; }
        public String Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Compensation()
        {
            CompensationId = Guid.NewGuid().ToString();
        }
    }
}