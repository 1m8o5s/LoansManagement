using System.ComponentModel.DataAnnotations;
using Loans.Frontend.Common;

namespace Loans.Frontend.Models;

public class LoanAddModel
{
    [Required]
    public string Customer { get; set; }

    [Required]
    [Range(default(double), 1000000)]
    public double LoanSum { get; set; }

    [Required]
    public CalculationType Type { get; set; }

    [Required]
    [Range(default(double), 100)]
    public double Interest { get; set; }

    [Required]
    [Range(default(int), 365)]
    public int Term { get; set; }
}