using Loans.Domain.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Loans.Domain.Entities
{
    public class Loan
    {
        [JsonProperty("id")]
        public Guid Id {get;set;}

        public string Customer { get; set; }

        public double LoanSum { get; set; }

        public CalculationType Type { get; set; }

        public double Interest { get; set; }

        public int Term { get; set; }

        public List<PaymentItem> PaymentGraph { get; set; }
    }
}
