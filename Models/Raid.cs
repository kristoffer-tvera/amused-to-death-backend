using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Raids")]
public class Raid : BaseEntity
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int Gold { get; set; }
    public bool Paid { get; set; }
    public string Comment { get; set; }
}