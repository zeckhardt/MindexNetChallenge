using Microsoft.VisualBasic;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public Employee Employee { get; set; }
        public double Salary { get; set; }
        public DateAndTime EffectiveDate { get; set; }
    }
}