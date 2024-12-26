using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

/*
********************************************************************
Exemplo da autenticacao do firebase
https://www.youtube.com/watch?v=jkTaHb0M4nw
****************************************************
*/
namespace ScreeningExchange.Hosts.Http.Firebase;
public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseApp firebase;
    #pragma warning disable CS0618
    public FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        FirebaseApp firebase)
        : base(options, logger, encoder, clock)
    {
        this.firebase = firebase;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string token = Context.Request.Headers["Authorization"]!;

        if (string.IsNullOrWhiteSpace(token))
            return AuthenticateResult.NoResult();

        if (token!.StartsWith("Bearer"))
            token = token.Substring("Bearer ".Length);

        try
        {
            FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(firebase).VerifyIdTokenAsync(token);

            return AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(
                        new List<ClaimsIdentity>()
                        {
                            new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(FirebaseAuthenticationHandler))
                        }
                    ),
                    JwtBearerDefaults.AuthenticationScheme
                )
            );
        }
        catch (System.Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }

    IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
        => claims.Select(
            c => new Claim(c.Key, c.Value?.ToString() ?? string.Empty)
        ).ToList();
    #pragma warning restore CS8618
}