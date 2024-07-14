using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models;

public record WowProfileResponse(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("wow_accounts")] IReadOnlyList<WowAccount> WowAccounts
);

public record ProfileCharacter(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("realm")] Realm Realm,
    [property: JsonPropertyName("playable_class")] PlayableClass PlayableClass,
    [property: JsonPropertyName("playable_race")] PlayableRace PlayableRace,
    [property: JsonPropertyName("gender")] Gender Gender,
    [property: JsonPropertyName("faction")] Faction Faction,
    [property: JsonPropertyName("level")] int? Level
);

public record Faction(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("name")] object Name
);

public record Gender(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("name")] object Name
);

public record PlayableClass(
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id
);

public record PlayableRace(
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id
);

public record Realm(
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("slug")] string Slug
);


public record WowAccount(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("characters")] IReadOnlyList<ProfileCharacter> Characters
);

