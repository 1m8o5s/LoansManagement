using Loans.Domain.Common.RoutingConfig;
using Loans.Domain.Entities.Identity;
using Loans.Domain.Maps;
using Loans.Domain.Models;
using Loans.Helpers.Identity.Contracts;
using Loans.Helpers.Response.Contracts;
using Loans.Service.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpResponseModelFactory _httpResponseModelFactory;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IJwtTokenManager jwtTokenManager,
            IHttpResponseModelFactory httpResponseModelFactory,
            IUserService userService
            )
        {
            _logger = logger;
            _jwtTokenManager = jwtTokenManager;
            _httpResponseModelFactory = httpResponseModelFactory;
            _userService = userService;
        }

        [HttpPost(UserEndpoints.ADD_USER)]
        public async Task<IActionResult> Add([FromBody] UserAuthenticateModel userModel)
        {
            try
            {
                await _userService.AddUserAsync(userModel);

                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(null, "User created successfully").ToJson());
            } catch (Exception exc)
            {
                const string message = "Error while adding user";

                _logger.LogError($"{message} {exc.Message}");

                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message));
            }
        }

        [HttpPost(UserEndpoints.AUTHORIZE_USER)]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateModel userModel)
        {
            try
            {
                User user = _userService.GetUserForGenerateToken(userModel);

                TokenWithExpireDateModel accessToken = _jwtTokenManager.GenerateJwtToken(user, user.Role);

                await _userService.UpdateTokenExpiringDateForUserAsync(user, accessToken.ExpireTime);

                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(accessToken).ToJson());
            } catch (Exception exc)
            {
                const string message = "Error while log in";

                _logger.LogError($"{message} {exc.Message}");

                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message));
            }
        }

        [HttpPost(UserEndpoints.UNAUTHORIZE_USER)]
        [Authorize]
        public async Task<IActionResult> LogOut([FromHeader] string authorization)
        {
            try
            {
                string userName = _jwtTokenManager.GetUserNameAuthorizedByToken(authorization);

                User user = _userService.GetUserByName(userName);

                await _userService.UpdateTokenExpiringDateForUserAsync(user, default);

                return new OkObjectResult(_httpResponseModelFactory.NewSuccessResponse(null).ToJson());
            } catch (Exception exc)
            {
                const string message = "Error while log out";

                _logger.LogError($"{message} {exc.Message}");

                return new BadRequestObjectResult(_httpResponseModelFactory.NewErrorResponse(message));
            }
        }

    }
}
