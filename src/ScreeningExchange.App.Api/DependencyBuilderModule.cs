

using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;
using ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;
using ScreeningExchange.App.Api.Features.Questions.FindAllFlows;
using ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;
using ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;
using ScreeningExchange.App.Api.Features.Questions.FindQuestionById;
using ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;
using ScreeningExchange.App.Api.Features.Questions.UpsertStudent;

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
            ;
}