namespace AmusedToDeath.Backend.Models;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ChangedDate { get; set; }
}