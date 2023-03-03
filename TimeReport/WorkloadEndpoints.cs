namespace TimeReport;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TimeReport.Data.Interface;
using TimeReport.Model;

public sealed class WorkloadEndpoints
{
    private readonly ILogger<WorkloadEndpoints> logger;
    private readonly ITimeReportService dbService;

    public WorkloadEndpoints(ILogger<WorkloadEndpoints> logger, ITimeReportService dbService)
    {
        this.logger = logger;
        this.dbService = dbService;
    }

    [OpenApiOperation(operationId: "GetWorkloads", tags: new[] { "Workloads" }, Summary = "GetWorkloads", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Workload))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetWorkloads")]
    public async Task<IActionResult> GetWorkloads(
        [HttpTrigger(AuthorizationLevel.Function, "get")]
        HttpRequestData req)
    {
        IEnumerable<Workload> workloads = await dbService.GetWorkloads();

        if (workloads is not null && workloads.Any())
        {
            return new OkObjectResult(workloads.ToArray());
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "GetWorkload", tags: new[] { "Workloads" }, Summary = "GetWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Workload))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetWorkload")]
    public async Task<IActionResult> GetWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Workload? workload = await dbService.GetWorkload(id);
            return workload is not null ? new OkObjectResult(workload) : new NotFoundResult();
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "CreateWorkload", tags: new[] { "Workloads" }, Summary = "CreateWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Workload))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Workload))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateWorkload")]
    public async Task<IActionResult> CreateWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Workload? workload = JsonSerializer.Deserialize<Workload>(req.Body, options);
            if (workload is not null)
            {
                await dbService.CreateWorkload(workload);
                return new OkObjectResult(workload);
            }
        }

        return new BadRequestResult();
    }


    [OpenApiOperation(operationId: "UpdateWorkload", tags: new[] { "Workloads" }, Summary = "UpdateWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Workload))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Workload))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("UpdateWorkload")]
    public async Task<IActionResult> UpdateWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Workload? workload = JsonSerializer.Deserialize<Workload>(req.Body, options);
            if (workload is not null)
            {
                await dbService.UpdateWorkload(workload);
                return new OkObjectResult(workload);
            }
        }

        return new BadRequestResult();
    }

    [OpenApiOperation(operationId: "DeleteWorkload", tags: new[] { "Workloads" }, Summary = "DeleteWorkload", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Workload))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteWorkload")]
    public async Task<IActionResult> DeleteWorkload(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Workload? workload = await dbService.DeleteWorkload(id);
            return workload is not null ? new OkObjectResult(workload) : new NotFoundResult();
        }

        return new NotFoundResult();
    }
}
