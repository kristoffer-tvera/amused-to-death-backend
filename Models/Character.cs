using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Characters")]
public class Character : BaseEntity
{
    public string Name { get; set; }
    public string Realm { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public int ItemLevel { get; set; }
    public int OwnerId { get; set; }

    [Computed]
    public Character Main { get; set; }
}