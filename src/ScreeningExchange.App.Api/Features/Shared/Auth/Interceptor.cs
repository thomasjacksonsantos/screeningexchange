using FastEndpoints;

namespace ScreeningExchange.App.Api.Features.Shared.Auth;

public class AuthInterceptor<TRequest>
    : IPreProcessor<TRequest> where TRequest : AuthRequest
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        var claims = ctx.HttpContext.User?.Claims.Select(x => new { x.Type, x.Value }).ToList();
        var uid = claims?.FirstOrDefault(x => x.Type == "user_id")?.Value;
        var name = claims?.FirstOrDefault(x => x.Type == "name")?.Value;
        var email = claims?.FirstOrDefault(x => x.Type == "email")?.Value;

        if (string.IsNullOrWhiteSpace(uid))
            throw new ArgumentException("user_id nao foi encontrado.");
            
        ctx.Request.Uid = uid;
        // ctx.Request.UserId = uid.ConvertUidToUlid();
        ctx.Request.UserName = name;
        ctx.Request.Email = email;

        return Task.CompletedTask;
    }
}