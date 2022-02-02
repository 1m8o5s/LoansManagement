using System;
using Loans.Domain.Entities;
using Loans.Domain.Models;
using Loans.Domain.Common;
using System.Collections.Generic;
using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Service.Calculations.Contracts;
using Loans.Helpers.Validation;
using Loans.Helpers.Validation.Contracts;

namespace Loans.Service.Calculations.CalculationPipeline
{
    public class PaymentGraphGenerator : IPaymentGraphGenerator
    {
        private IValidatorBuilderFactory _validatorBuilderFactory;

        public PaymentGraphGenerator(IValidatorBuilderFactory validatorBuilderFactory)
        {
            _validatorBuilderFactory = validatorBuilderFactory;
        }

        public IEnumerable<PaymentItem> GeneratePaymentItems(ICalculationMethod calculationMethod, LoanAddModel loanToGenerateGraph)
        {
            const int minimalLoanPeriodForFloatingRate = 36;
            const int minimalInterestForFloatingRate = 5;

            Validator<LoanAddModel> validator = _validatorBuilderFactory.NewValidatorBuilder<LoanAddModel>()
                    .AddRule(model => model != null)
                    .AddRule(model => model.LoanSum > 0 && model.Interest > 0 && model.Term > 0)
                    .AddRule(model => !string.IsNullOrEmpty(model.Customer))
                    .AddRule(model => (model.Type == CalculationType.FloatingRate && model.Term >= minimalLoanPeriodForFloatingRate && model.Interest >= minimalInterestForFloatingRate) || model.Type != CalculationType.FloatingRate)
                    .Build();

            if (!validator.IsValid(loanToGenerateGraph))
            {
                throw new Exception("Invalid Loan Model");
            }

            PaymentItem currentPaymentItem = calculationMethod.CalculateInitialPaymentItem(loanToGenerateGraph);

            yield return currentPaymentItem;
            
            int numberOfItemsForGraph = loanToGenerateGraph.Term;

            for (int _ = 1; _ < numberOfItemsForGraph; _++)
            {
                currentPaymentItem = calculationMethod.CalculateNextPaymentItem(currentPaymentItem, loanToGenerateGraph);

                yield return currentPaymentItem;
            }
        }
    }
}
