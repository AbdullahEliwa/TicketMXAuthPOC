using TicketMXAuthPOC.DTOs;

namespace TicketMXAuthPOC.Services
{
    public interface ITicketMXService
    {
        Task<TokenResponseModel> Register(RegisterDto dto);
        Task<TokenResponseModel> Login(LoginDto dto);
        Task<string> GetEvent(string accessToken);
    }
}
