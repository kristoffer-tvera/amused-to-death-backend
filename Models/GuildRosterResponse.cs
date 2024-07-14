using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models;

public record Guild(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("realm")] Realm Realm,
    [property: JsonPropertyName("faction")] Faction Faction
);

public record Member(
    [property: JsonPropertyName("character")] ProfileCharacter Character,
    [property: JsonPropertyName("rank")] int? Rank
);

public record GuildRosterResponse(
    [property: JsonPropertyName("guild")] Guild Guild,
    [property: JsonPropertyName("members")] IReadOnlyList<Member> Members
);
