
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Domain.AuthsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public class UseCase
(
    ISchoolRepository schoolRepository,
    IAuthRepository authRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<SignUpResponse>> outputPort
)
    : IInputOutputPortUseCase<SignUpRequest, IUseCaseOutputPort<Result<SignUpResponse>>, Result<SignUpResponse>>
{
    private readonly ISchoolRepository schoolRepository = schoolRepository;
    private readonly IAuthRepository authRepository = authRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<SignUpResponse>> OutputPort = outputPort;

    public async ValueTask<Result<SignUpResponse>> Execute(
        SignUpRequest input,
        CancellationToken ct = default
    )
    {
        var userRecord = await authRepository.CreateUserAsync(
            input.Email,
            input.Password,
            input.Displayname
        );

        var authToken = await authRepository.SignInWithEmailPassword(
              input.Email.ToLower(),
              input.Password.ToLower()
          );

        return OutputPort.Ok(new SignUpResponse(
                authToken.IdToken
            )
        );
    }
}
