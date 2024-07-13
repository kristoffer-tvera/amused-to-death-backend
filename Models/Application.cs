using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Applications")]
public class Application
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public string Spec { get; set; }
    public string Realm { get; set; }
    public string InterfaceUrl { get; set; }
    public string LogsUrl { get; set; }
    public string Comment { get; set; }
    public string Alts { get; set; }
    public string ChangeKey { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ChangedDate { get; set; }
}