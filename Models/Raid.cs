using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Raids")]
public class Raid
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int Gold { get; set; }
    public int Paid { get; set; }
    public string Comment { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ChangedDate { get; set; }
}