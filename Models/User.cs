using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public bool Officer { get; set; }
    public string BattleTag { get; set; }

    [Computed]
    public virtual ICollection<Character> Characters { get; set; }
}