
namespace ScreeningExchange.Domain.AuthsAggregate;

public interface IAuthRepository
{
    ValueTask<AuthToken> SignInWithEmailPassword(
        string email,
        string password
    );

    ValueTask<UserRecord> CreateUserAsync(
        string email,
        string password,
        string displayName
    );
}
