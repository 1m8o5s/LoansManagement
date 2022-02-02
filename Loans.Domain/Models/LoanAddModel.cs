using Loans.Domain.Common;

namespace Loans.Domain.Models
{
    public class LoanAddModel
    {
        public string Customer { get; set; }
        
        public double LoanSum { get; set; }
        
        public CalculationType Type { get; set; }
        
        public double Interest { get; set; }
        
        public int Term { get; set; }
    }
}
