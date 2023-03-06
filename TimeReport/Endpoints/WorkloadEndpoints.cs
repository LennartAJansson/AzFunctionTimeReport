namespace TimeReport.Endpoints;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using TimeReport.Contract;
using TimeReport.Model;

public sealed class WorkloadEndpoints
{
    private readonly ILogger<WorkloadEndpoints> logger;
    private readonly IMediator mediator;

    public WorkloadEndpoints(ILogger<WorkloadEndpoints> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [OpenApiOperation(operationId: "ReadWorkloads", tags: new[] { "Workloads" }, Summary = "ReadWorkloads", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<WorkloadResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkloads")]
    public async Task<IActionResult> ReadWorkloads(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        IEnumerable<WorkloadResponse> response = await mediator.Send(new ReadWorkloadsQuery());

        return response is not null && response.Any() ? new OkObjectResult(response.ToArray()) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadWorkload", tags: new[] { "Workloads" }, Summary = "ReadWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(WorkloadFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkload")]
    public async Task<IActionResult> ReadWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        WorkloadFullResponse? response = await mediator.Send(new ReadWorkloadQuery(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "CreateWorkload", tags: new[] { "Workloads" }, Summary = "CreateWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(CreateWorkloadCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateWorkload")]
    public async Task<IActionResult> CreateWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            CreateWorkloadCommand? request = JsonSerializer.Deserialize<CreateWorkloadCommand>(req.Body, options);
            if (request is not null)
            {
                WorkloadResponse response = await mediator.Send(request);
                return new OkObjectResult(response);
            }
        }

        return new BadRequestResult();
    }


    [OpenApiOperation(operationId: "UpdateWorkload", tags: new[] { "Workloads" }, Summary = "UpdateWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(UpdateWorkloadCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("UpdateWorkload")]
    public async Task<IActionResult> UpdateWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            UpdateWorkloadCommand? request = JsonSerializer.Deserialize<UpdateWorkloadCommand>(req.Body, options);
            if (request is not null)
            {
                WorkloadResponse response = await mediator.Send(request);
                return new OkObjectResult(response);
            }
        }

        return new BadRequestResult();
    }

    [OpenApiOperation(operationId: "DeleteWorkload", tags: new[] { "Workloads" }, Summary = "DeleteWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteWorkload")]
    public async Task<IActionResult> DeleteWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        WorkloadResponse? response = await mediator.Send(new DeleteWorkloadCommand(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }
}
