using Loans.Domain.Entities;
using Loans.Domain.Models;
using Loans.Helpers.Validation;
using Loans.Helpers.Validation.Contracts;
using Loans.Service.Calculations;
using Loans.Service.Calculations.CalculationPipeline;
using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Service.Calculations.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loans.Test;

[TestFixture]
public class Calculation
{
    private ICalculationMethodFactory _calculationMethodFactory;
    
    private IPaymentGraphGeneratorFactory _paymentGraphGeneratorFactory;

    private List<LoanAddModel> _loanAddModel;

    public Calculation()
    {
        _calculationMethodFactory = new CalculationMethodFactory();

        IValidatorBuilderFactory validatorBuilderFactory = new ValidatorBuilderFactory();

        _paymentGraphGeneratorFactory = new PaymentGraphGeneratorFactory(validatorBuilderFactory);
    }

    [SetUp]
    public void SetUp()
    {
        _loanAddModel = new List<LoanAddModel>
        {
            new LoanAddModel
            {
                Customer = "Test",
                Interest = 5,
                LoanSum = 10000,
                Term = 36,
                Type = Domain.Common.CalculationType.EqualPricing
            },

            new LoanAddModel
            {
                Customer = "Test",
                Interest = 5,
                LoanSum = 18000,
                Term = 20,
                Type = Domain.Common.CalculationType.Annuity
            },

            new LoanAddModel
            {
                Customer = "Test",
                Interest = 9,
                LoanSum = 25000,
                Term = 18,
                Type = Domain.Common.CalculationType.FloatingRate
            },

            new LoanAddModel
            {
                Customer = "Test",
                Interest = 6,
                LoanSum = 18000,
                Term = 40,
                Type = Domain.Common.CalculationType.FloatingRate
            },

            new LoanAddModel
            {
                Customer = "Test",
                Interest = 6,
                LoanSum = 18000,
                Term = 40,
                Type = (Domain.Common.CalculationType)6
            }
        };
    }

    [Test]
    public void CheckCalculationResultForFloatingRateWithLessThan_MustThrowException()
    {
        LoanAddModel model = _loanAddModel[2];

        ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(model.Type);

        IPaymentGraphGenerator paymentGraphGenerator = _paymentGraphGeneratorFactory.NewPaymentGenerator();

        Assert.Throws<Exception>(() => paymentGraphGenerator.GeneratePaymentItems(calculationMethod, model).ToList());
    }

    [Test]
    public void CheckCalculationResultForUnsupportedCalculationMethod_MustThrowInvalidOperationException()
    {
        LoanAddModel model = _loanAddModel[4];

        ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(model.Type);

        IPaymentGraphGenerator paymentGraphGenerator = _paymentGraphGeneratorFactory.NewPaymentGenerator();

        Assert.Throws<InvalidOperationException>(() => paymentGraphGenerator.GeneratePaymentItems(calculationMethod, model).ToList());
    }

    [Test]
    public void CheckBalanceOfPaymentGraphItemInForEightMonthForAnnuityModel_MustBeEqualWithExpected()
    {
        double expected = 11869.54;

        LoanAddModel model = _loanAddModel[1];

        ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(model.Type);

        IPaymentGraphGenerator paymentGraphGenerator = _paymentGraphGeneratorFactory.NewPaymentGenerator();


        List<PaymentItem> paymentGraph = paymentGraphGenerator.GeneratePaymentItems(calculationMethod, model).ToList();

        double actual = paymentGraph[7].Balance;

        
        Assert.AreEqual(expected, actual, 0.01);
    }

    [Test]
    public void CheckTotalOfPaymentGraphItemInForNineteenMonthForEqualPricingModel_MustBeEqualWithExpected()
    {
        double expected = 298.61;

        LoanAddModel model = _loanAddModel[0];

        ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(model.Type);

        IPaymentGraphGenerator paymentGraphGenerator = _paymentGraphGeneratorFactory.NewPaymentGenerator();


        List<PaymentItem> paymentGraph = paymentGraphGenerator.GeneratePaymentItems(calculationMethod, model).ToList();

        double actual = paymentGraph[18].Total;

        Assert.AreEqual(expected, actual, 0.01);
    }

    [Test]
    public void CheckBalanceOfPaymentGraphItemInForTwentiethMonthForFloatingRate_MustBeEqualWithExpected()
    {
        double expected = 9450.00;

        LoanAddModel model = _loanAddModel[3];

        ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(model.Type);

        IPaymentGraphGenerator paymentGraphGenerator = _paymentGraphGeneratorFactory.NewPaymentGenerator();


        List<PaymentItem> paymentGraph = paymentGraphGenerator.GeneratePaymentItems(calculationMethod, model).ToList();

        double actual = paymentGraph[19].Balance;

        Assert.AreEqual(expected, actual, 0.01);
    }
}