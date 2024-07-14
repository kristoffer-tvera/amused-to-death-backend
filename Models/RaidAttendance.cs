using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Models;

[Table("RaidAttendance")]
public class RaidAttendance : BaseEntity
{
    public int RaidId { get; set; }
    public int CharacterId { get; set; }
    public int BossesKilled { get; set; }
    public bool Paid { get; set; }
}