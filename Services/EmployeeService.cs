using ApiGatewayUser.Model;
using buildingBlocksCore.Utils;
using BuildingBlocksCore.Models;
using BuildingBlocksCore.Utils;
using BuildingBlocksServices.Models;
using System.Linq.Expressions;

namespace ApiGatewayUser.Services
{

    public interface IEmployeeService
    {
        Task<BaseResponseApi<UpdateUserResponse>> UpdateCustomer(UpdateUserRequest updateUserRequest);

        Task<PagedDataResponse<EmployeeDto>> Get(UserListRequest userListRequest);


    }
    public class EmployeeService : BaseServiceApiGateway, IEmployeeService
    {
        readonly HttpClient _httpClient;
        public EmployeeService(LNotifications notification, HttpClient httpClient) 
            : base(notification)
        {
            _httpClient = httpClient;   
        }

        public async Task<PagedDataResponse<EmployeeDto>> Get(UserListRequest userListRequest)
        {
            var ret  = new PagedDataResponse<EmployeeDto>();    
            await Task.FromResult(true);
            
            return ret; 
        }

        public async Task<BaseResponseApi<UpdateUserResponse>> UpdateCustomer(UpdateUserRequest updateUserRequest)
        {

            var httpContent = GetContentJsonUTF8(updateUserRequest);
            var responseLogin = await _httpClient.PutAsync($"api/v1/emplyoee", httpContent);
            await TreatErrorsResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin);
            if (_notification.Any())
                return null;
            return await DeserializeObjResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin);
        }
    }
}
