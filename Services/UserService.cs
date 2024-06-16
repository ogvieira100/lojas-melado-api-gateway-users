using ApiGatewayUser.Model;
using buildingBlocksCore.Utils;
using BuildingBlocksCore.Utils;
using BuildingBlocksServices.Models;

namespace ApiGatewayUser.Services
{

    public interface IUserService
    {
        Task<BaseResponseApi<UserRegisterResponse>> UserRegisterRequestAsync(UserRegisterRequest userRegisterRequest);

        Task<BaseResponseApi<UserLoginDto>> LoginAsync(UserLoginRequest userLoginRequest);

        Task<BaseResponseApi<object>> DeleteUserAsync(Guid id);
    }
    public class UserService : BaseServiceApiGateway, IUserService
    {
        readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient, LNotifications notification) : base(notification)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseApi<object>> DeleteUserAsync(Guid id)
        {
            var responseLogin = await _httpClient.DeleteAsync($"api/v1/user/deletar-conta/{id}");
            await TreatErrorsResponse<object>(responseLogin);

            return new BaseResponseApi<object>();
        }

        public async Task<BaseResponseApi<UserLoginDto>> LoginAsync(UserLoginRequest userLoginRequest)
        {
            var responseLogin = await _httpClient.GetAsync($"api/v1/user/login?{userLoginRequest.GetQueryString()}");
            await TreatErrorsResponse<UserLoginDto>(responseLogin);
            if (_notification.Any())
                return null;
            return (await DeserializeObjResponse<BaseResponseApi<UserLoginDto>>(responseLogin));
        }
        public async Task<BaseResponseApi<UserRegisterResponse>> UserRegisterRequestAsync(UserRegisterRequest userRegisterRequest)
        {
            var httpContent = GetContentJsonUTF8(userRegisterRequest);
            var responseLogin = await _httpClient.PostAsync($"api/v1/user/nova-conta", httpContent);
            await TreatErrorsResponse<UserRegisterDto>(responseLogin);
            if (_notification.Any())
                return null;
            return (await DeserializeObjResponse<BaseResponseApi<UserRegisterResponse>>(responseLogin));
        }
    }
}
