using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("Applications")]
public class Application : BaseEntity
{
    public string Name { get; set; }
    public string Class { get; set; }
    public string Spec { get; set; }
    public string Realm { get; set; }
    public string InterfaceUrl { get; set; }
    public string LogsUrl { get; set; }
    public string Comment { get; set; }
    public string Alts { get; set; }
    public string ChangeKey { get; set; }
}