﻿using Loans.Frontend.Common;

namespace Loans.Frontend.Models;

public class LoanListModel
{
    public Guid Id { get; set; }

    public string Customer { get; set; }

    public double LoanSum { get; set; }

    public CalculationType Type { get; set; }

    public double Interest { get; set; }

    public int Term { get; set; }
}