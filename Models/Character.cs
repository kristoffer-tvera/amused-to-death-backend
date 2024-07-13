using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Characters")]
public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Realm { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public int ItemLevel { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ChangedDate { get; set; }
    public int OwnerId { get; set; }
}