using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Users")]
public class User : BaseEntity
{
    public bool Officer { get; set; }
    public string BattleTag { get; set; }
    public int? PrimaryCharacterId { get; set; }

    [Computed]
    public Character PrimaryCharacter { get; set; }

    [Computed]
    public virtual ICollection<Character> Characters { get; set; }
}