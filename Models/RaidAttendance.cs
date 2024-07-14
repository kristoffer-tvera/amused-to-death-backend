using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("RaidAttendance")]
public class RaidAttendance
{
    public int Id { get; set; }
    public int RaidId { get; set; }
    public int CharacterId { get; set; }
    public int BossesKilled { get; set; }
    public bool Paid { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ChangedDate { get; set; }
}