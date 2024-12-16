
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public class UseCase
(
    ISchoolRepository schoolRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<FindAllSchoolResponse>> outputPort
)
    : IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>>
{
    private readonly ISchoolRepository schoolRepository = schoolRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<FindAllSchoolResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindAllSchoolResponse>> Execute(
        FindAllSchoolRequest input,
        CancellationToken ct = default
    )
    {
        var schools = await schoolRepository.FindAllAsync(
            input.Page,
            input.Total
        );

        return OutputPort.Ok(new FindAllSchoolResponse(
            schools?.Select(c => new SchoolResponse(
                c.Id.ToString(),
                c.Name.Value,
                c.Email.Value,
                c.Phone.Value
                )
            )
        ));
    }
}
