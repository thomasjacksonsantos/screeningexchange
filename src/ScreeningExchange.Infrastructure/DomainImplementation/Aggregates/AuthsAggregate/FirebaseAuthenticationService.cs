using System.Net.Http.Json;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using ScreeningExchange.Domain.AuthsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AuthsAggregate;

public sealed class FirebaseAuthenticationService(
    IHttpClientFactory factory,
    IOptions<Configuration.ApiConfig> options
)
    : HttpClientBase(factory, options.Value.FirebaseAuthentication.ServiceName), IAuthRepository
{
    private readonly Configuration.ApiConfig apiConfig = options.Value;

    public async ValueTask<Domain.AuthsAggregate.UserRecord> CreateUserAsync(
        string email,
        string password,
        string displayName
    )
    {
        var userRecordArgs = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
        {
            Email = email.ToLower(),
            Password = password.ToLower(),
            DisplayName = displayName.ToLower()
        });


        var authToken = await SignInWithEmailPassword(
            email.ToLower(),
            password.ToLower()
        );

        return new Domain.AuthsAggregate.UserRecord
        (
            userRecordArgs.Uid,
            authToken.IdToken
        );
    }

    public async ValueTask<AuthToken> SignInWithEmailPassword(
        string email,
        string password
    )
    {
        var requestData = new
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        HttpResponseMessage response = await Client.PostAsJsonAsync(
            apiConfig.FirebaseAuthentication.TokenUri,
            requestData
        );

        if (response.IsSuccessStatusCode is false)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<AuthToken>() ?? null!;
    }
}