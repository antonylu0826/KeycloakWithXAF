using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeycloakWithXAF.WebApi.API.Security
{
    public class KeycloakAuthService
    {
        readonly HttpClient HttpClient;
        readonly IConfiguration Configuration;

        public KeycloakAuthService(IServiceProvider serviceProvider, HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;
            Configuration = configuration;
        }

        public async Task<TokenResponse> LoginAsync(string username, string password)
        {
            var tokenEndpoint = $"{Configuration["Keycloak:Authority"]}/protocol/openid-connect/token";

            var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", Configuration["Keycloak:ClientId"] },
            { "client_secret", Configuration["Keycloak:ClientSecret"] },
            { "username", username },
            { "password", password }
        };

            var content = new FormUrlEncodedContent(requestBody);
            var response = await HttpClient.PostAsync(tokenEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                return tokenResult!;
            }
            throw new Exception("failed");
        }

    }

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }

}
