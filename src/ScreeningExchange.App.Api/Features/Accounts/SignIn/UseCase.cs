
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Domain.AuthsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public class UseCase
(
    ISchoolRepository schoolRepository,
    IAuthRepository authRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<SignInResponse>> outputPort
)
    : IInputOutputPortUseCase<SignInRequest, IUseCaseOutputPort<Result<SignInResponse>>, Result<SignInResponse>>
{
    private readonly ISchoolRepository schoolRepository = schoolRepository;
    private readonly IAuthRepository authRepository = authRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<SignInResponse>> OutputPort = outputPort;

    public async ValueTask<Result<SignInResponse>> Execute(
        SignInRequest input,
        CancellationToken ct = default
    )
    {
        var authToken = await authRepository.SignInWithEmailPassword(
              input.Email.ToLower(),
              input.Password.ToLower()
          );

        return OutputPort.Ok(new SignInResponse(
                authToken.IdToken
            )
        );
    }
}
