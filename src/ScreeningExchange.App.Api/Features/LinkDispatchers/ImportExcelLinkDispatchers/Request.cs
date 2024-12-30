
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public record ImportExcelLinkDispatchersRequest(
    string BuildQuestionId,
    [FromForm] IFormFile File
) : AuthRequest;

