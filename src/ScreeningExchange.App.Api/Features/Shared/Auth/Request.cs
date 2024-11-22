using System.Text.Json.Serialization;

namespace ScreeningExchange.App.Api.Features.Shared.Auth;

public record AuthRequest
{
    [JsonIgnore]
    public string? Uid { get; set; }
    [JsonIgnore]
    public Ulid UserId { get; set; }
    [JsonIgnore]
    public string? UserName { get; set; }
    [JsonIgnore]
    public string? Email { get; set; }
}