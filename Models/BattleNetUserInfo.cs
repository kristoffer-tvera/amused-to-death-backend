using System.Text.Json.Serialization;

namespace AmusedToDeath.Backend.Models
{

    public class BattleNetUserInfo
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("battletag")]
        public string BattleTag { get; set; }

        [JsonPropertyName("sub")]
        public string Sub { get; set; }
    }
}