using ApiGatewayUser.Model;
using ApiGatewayUser.Services;
using Asp.Versioning;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Utils;
using BuildingBlocksServices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayUser.V1.Controllers
{


    [ApiVersion("1.0")]
    [Microsoft.AspNetCore.Components.Route("api/v{version:apiVersion}/apiGatewayUser")]
    [Authorize]
    public class UserController : MainController
    {
        readonly IEmployeeService _employeeService;
        readonly IUserService _userService;
        public UserController(IEmployeeService employeeService,
            IUserService userService,
            LNotifications notifications)
            : base(notifications)
        {
            _employeeService = employeeService;
            _userService = userService; 
        }

        [HttpGet("list-users")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Get([FromQuery] UserListRequest userListRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerAsync(() => _employeeService.Get(userListRequest));
        }


        [HttpPost("nova-conta")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegister)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.UserRegisterRequestAsync(userRegister));

        }

        [HttpPut("atualizar-conta")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _employeeService.UpdateCustomer(updateUserRequest));

        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.DeleteUserAsync(id));

        }


        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.LoginAsync(userLogin));
        }

    }
}
