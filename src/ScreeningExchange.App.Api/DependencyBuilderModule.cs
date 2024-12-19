

using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.App.Api.Features.Accounts.SignIn;
using ScreeningExchange.App.Api.Features.Accounts.SignUp;
using ScreeningExchange.App.Api.Features.Agents.FindAgentById;
using ScreeningExchange.App.Api.Features.Agents.UpsertAgent;
using ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;
using ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;
using ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;
using ScreeningExchange.App.Api.Features.Questions.FindAllFlows;
using ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;
using ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;
using ScreeningExchange.App.Api.Features.Questions.FindQuestionById;
using ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;
using ScreeningExchange.App.Api.Features.Schools.FindAllSchool;
using ScreeningExchange.App.Api.Features.Schools.FindSchoolById;
using ScreeningExchange.App.Api.Features.Schools.UpsertSchool;
using ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

namespace ScreeningExchange.App.Api;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
        => services
            .AddFindQuestionByIdFeature()
            .AddUpsertQuestionFeature()
            .AddFindAllQuestionFeature()
            .AddFindNextQuestionByAwnserFeature()
            .AddFindAllFlowsFeature()
            .AddStudentUpsertStudentFeature()
            .AddUpsertDestinationFeature()
            .AddFindDestinationByStudentIdFeature()
            .AddFindAllDestinationForSchoolFeature()
            .AddStudentFindAllSchoolFeature()
            .AddStudentFindSchoolByIdFeature()
            .AddUpsertSchoolFeature()
            .AddSignUpFeature()
            .AddSignInFeature()
            .AddStudentUpsertAgentFeature()
            .AddStudentFindAgentByIdFeature()
            ;
}