namespace AmusedToDeath.Backend.Models
{
    public record LoginRequest(string Code, string? State);
}