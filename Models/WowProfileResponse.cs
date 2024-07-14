using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models;

public record WowProfileResponse(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("wow_accounts")] IReadOnlyList<WowAccount> WowAccounts
);

public record WowAccount(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("characters")] IReadOnlyList<ProfileCharacter> Characters
);

