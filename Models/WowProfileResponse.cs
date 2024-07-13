using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models;

public record ProfileCharacter(
    [property: JsonPropertyName("character")] Key Character,
    [property: JsonPropertyName("protected_character")] Key ProtectedCharacter,
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

public record Key(
    [property: JsonPropertyName("href")] string Href
);

public record Links(
    [property: JsonPropertyName("self")] Key Self,
    [property: JsonPropertyName("user")] User User,
    [property: JsonPropertyName("profile")] Profile Profile
);

public record PlayableClass(
    [property: JsonPropertyName("key")] Key Key,
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id
);

public record PlayableRace(
    [property: JsonPropertyName("key")] Key Key,
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id
);

public record Profile(
    [property: JsonPropertyName("href")] string Href
);

public record Realm(
    [property: JsonPropertyName("key")] Key Key,
    [property: JsonPropertyName("name")] object Name,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("slug")] string Slug
);

public record WowProfileResponse(
    [property: JsonPropertyName("_links")] Links Links,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("wow_accounts")] IReadOnlyList<WowAccount> WowAccounts,
    [property: JsonPropertyName("collections")] Key Collections
);

public record WowAccount(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("characters")] IReadOnlyList<ProfileCharacter> Characters
);

