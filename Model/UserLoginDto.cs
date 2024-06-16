namespace ApiGatewayUser.Model
{
    public class UserClaimDto
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
    public class UserTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimDto> Claims { get; set; }
        public string Name { get; set; }

        public UserTokenDto()
        {
            Claims = new List<UserClaimDto>();
        }
    }
    public class UserLoginDto
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
