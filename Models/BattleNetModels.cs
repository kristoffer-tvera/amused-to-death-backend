using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models;


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
    [property: JsonPropertyName("name")] string Name
);

public record Gender(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("name")] string Name
);

public record PlayableClass(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int? Id
);

public record PlayableRace(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int? Id
);

public record Realm(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("slug")] string Slug
);


