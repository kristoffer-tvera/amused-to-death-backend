using System.Net.Http.Headers;
using System.Text;
using AmusedToDeath.Backend.Models;

namespace AmusedToDeath.Backend.Services;

public class BattleNetService(HttpClient httpClient, ILogger<BattleNetService> logger, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<BattleNetService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    // public async Task<Character> GetCharacterAsync(string realm, string name)
    // {
    //     var response = await _httpClient.GetAsync($"https://eu.api.blizzard.com/profile/wow/character/{realm}/{name.ToLower()}?namespace=profile-eu&locale=en_US&access_token={_httpClient.DefaultRequestHeaders.Authorization?.Parameter}");

    //     if (!response.IsSuccessStatusCode)
    //     {
    //         _logger.LogError($"Failed to get character {name} from realm {realm}. Status code: {response.StatusCode}");
    //         return null;
    //     }

    //     var character = await response.Content.ReadFromJsonAsync<Character>();

    //     return character;
    // }

    public async Task<string> GetAccessToken(string code)
    {
        var authroizationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_configuration["BattleNet:ClientId"]}:{_configuration["BattleNet:ClientSecret"]}")));

        _httpClient.DefaultRequestHeaders.Authorization = authroizationHeader;
        var response = await _httpClient.PostAsync("https://oauth.battle.net/token", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = code,
            ["redirect_uri"] = "http://localhost:5173/auth"
        })
        );

        if (!response.IsSuccessStatusCode)
        {
            var responseMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Failed to get access token. Status code: {response.StatusCode}, response: {responseMessage}");
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<BattleNetTokenResponse>();

        _logger.LogInformation($"Access token: {tokenResponse.AccessToken}");

        return tokenResponse.AccessToken;
    }

    public async Task<BattleNetUserInfo?> GetUserInfo(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://oauth.battle.net/oauth/userinfo");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to get BattleNet user. Status code: {response.StatusCode}");
            return null;
        }

        var user = await response.Content.ReadFromJsonAsync<BattleNetUserInfo>();

        _logger.LogInformation($"BattleNet user {user?.BattleTag} successfully authenticated");

        return user;
    }

    public async Task<WowProfileResponse> GetProfileAsync(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync($"https://eu.api.blizzard.com/profile/user/wow?namespace=profile-eu&locale=en_GB");

        if (!response.IsSuccessStatusCode)
        {
            var responseMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Failed to get profile. Status code: {response.StatusCode}, response: {responseMessage}");
            return null;
        }

        var profile = await response.Content.ReadFromJsonAsync<WowProfileResponse>();

        return profile;
    }

    public async Task<GuildRosterResponse> GetGuildRosterAsync(string accessToken, string realm, string guildName)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync($"https://eu.api.blizzard.com/data/wow/guild/{realm}/{guildName}/roster?namespace=profile-eu&locale=en_GB");

        if (!response.IsSuccessStatusCode)
        {
            var responseMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Failed to get guild roster. Status code: {response.StatusCode}, response: {responseMessage}");
            return null;
        }

        var roster = await response.Content.ReadFromJsonAsync<GuildRosterResponse>();

        return roster;
    }
}
