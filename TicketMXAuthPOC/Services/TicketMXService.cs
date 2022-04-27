using Marvin.StreamExtensions;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Text.Json;
using TicketMXAuthPOC.DTOs;
using TicketMXAuthPOC.Models;

namespace TicketMXAuthPOC.Services
{
    public class TicketMXService : ITicketMXService
    {
        #region CTOR, Fields.
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketMXService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Register
        public async Task<TokenResponseModel> Register(RegisterDto dto)
        {
            var httpClient = _clientFactory.CreateClient("TicketMXApiClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "register");

            var serializedData = JsonSerializer.Serialize(dto);
            request.Content = new StringContent(serializedData);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var tokenResponseModel = new TokenResponseModel();
            using (var response = await httpClient.SendAsync(request,
               HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                tokenResponseModel = stream.ReadAndDeserializeFromJson<TokenResponseModel>();
            }
            return tokenResponseModel;
        }

        #endregion

        #region Login
        public async Task<TokenResponseModel> Login(LoginDto dto)
        {
            var httpClient = _clientFactory.CreateClient("TicketMXApiClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "login");

            var serializedData = JsonSerializer.Serialize(dto);
            request.Content = new StringContent(serializedData);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var tokenResponseModel = new TokenResponseModel();
            using (var response = await httpClient.SendAsync(request,
               HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                tokenResponseModel = stream.ReadAndDeserializeFromJson<TokenResponseModel>();
            }
            return tokenResponseModel;
        }
        #endregion

        #region Get Event
        public async Task<string> GetEvent(string accessToken)
        {
            var httpClient = _clientFactory.CreateClient("TicketMXClient");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/ar/dt/687?authorization={accessToken}");

            var responsePage = string.Empty;
            using (var response = await httpClient.SendAsync(request,
               HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                responsePage = await response.Content.ReadAsStringAsync();
            }
            return responsePage;
        }
        #endregion

    }
}
