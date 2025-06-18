namespace Back.WebApi.DTOs.Requests
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = default!;
    }
}
