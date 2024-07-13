using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public int Officer { get; set; }
    public string BattleTag { get; set; }

    public List<Character> Characters { get; set; }
}