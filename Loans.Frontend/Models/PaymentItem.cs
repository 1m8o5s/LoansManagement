namespace Loans.Frontend.Models;

public class PaymentItem
{
    public int Month { get; set; }

    public double Balance { get; set; }

    public double Interest { get; set; }

    public double Payment { get; set; }
        
    public double Total { get; set; }
}