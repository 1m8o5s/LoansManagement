using Loans.Domain.Common.RoutingConfig;
using Loans.Domain.Entities;
using Loans.Domain.Maps;
using Loans.Domain.Models;
using Loans.Helpers.Response.Contracts;
using Loans.Helpers.Validation.Contracts;
using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Service.Calculations.Contracts;
using Loans.Service.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IPaymentGraphGeneratorFactory _paymentGraphGeneratorFactory;
        private readonly ICalculationMethodFactory _calculationMethodFactory;
        private readonly IHttpResponseModelFactory _httpResponseModelFactory;
        private readonly ILogger<LoansController> _logger;
        private readonly ILoanService _dataService;

        public LoansController(
            IPaymentGraphGeneratorFactory paymentGraphGeneratorFactory,
            ICalculationMethodFactory calculationMethodFactory,
            IValidatorBuilderFactory validatorBuilderFactory,
            IHttpResponseModelFactory httpResponseModelFactory,
            ILogger<LoansController> logger,
            ILoanService dataService
            )
        {
            _paymentGraphGeneratorFactory = paymentGraphGeneratorFactory;
            _calculationMethodFactory = calculationMethodFactory;
            _httpResponseModelFactory = httpResponseModelFactory;
            _dataService = dataService;
            _logger = logger;
        }

        [HttpPost(LoanEndpoints.ADD)]
        public async Task<IActionResult> Add([FromBody] LoanAddModel loanModelToAdd)
        {
            try
            {
                ICalculationMethod calculationMethod = _calculationMethodFactory.NewCalculationMethod(loanModelToAdd.Type);

                List<PaymentItem> paymentGraph = _paymentGraphGeneratorFactory
                    .NewPaymentGenerator()
                    .GeneratePaymentItems(calculationMethod, loanModelToAdd)
                    .ToList();

                Guid loanIdToReturn = (await _dataService.AddLoanWithCalculatedPaymentGraphs(loanModelToAdd, paymentGraph)).Id;
                
                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(loanIdToReturn.ToString()).ToJson());
            }
            catch (Exception exc)
            {
                const string message = "Error while adding loan";

                _logger.LogError($"{message}: {exc.Message}");
                
                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message).ToJson());
            }
        }

        [HttpGet(LoanEndpoints.GET)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            try
            {
                Loan loanToReturn = await _dataService.GetLoan(id);

                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(loanToReturn).ToJson());
            } catch (Exception exc)
            {
                const string message = "Error while getting loan";

                _logger.LogError($"{message}: {exc.Message}");

                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message).ToJson());
            }
        }
        
        [HttpGet(LoanEndpoints.LIST)]
        public async Task<IActionResult> All()
        {
            try
            {
                List<LoanListModel> loansToReturn = await _dataService.GetAllLoans();

                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(loansToReturn).ToJson());
            } catch (Exception exc)
            {
                const string message = "Error while loading loan list";
                
                _logger.LogError($"{message}: {exc.Message}");
                
                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message).ToJson());
            }
        }
    }
}
