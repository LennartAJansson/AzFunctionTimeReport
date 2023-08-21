namespace TimeReport.Functions.Endpoints;
using System.Net;

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

public sealed class Workloads : BaseHttpFunction
{
    private readonly ILogger logger;
    private readonly IMediator mediator;

    public Workloads(ILoggerFactory loggerFactory, IMediator mediator)
    {
        logger = loggerFactory.CreateLogger<Workloads>();
        this.mediator = mediator;
    }

    [OpenApiOperation(operationId: "CreateWorkload", tags: new[] { "Workloads" })]
    [OpenApiRequestBody("application/json", typeof(CreateWorkloadCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateWorkload")]
    public async Task<HttpResponseData> CreateWorkload([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        CreateWorkloadCommand? request = await GetFromBody<CreateWorkloadCommand>(req.Body);
        if (request is null)
        {
            return req.BadRequestResult();
        }

        WorkloadResponse response = await mediator.Send(request);

        return response is not null?
            req.OkObjectResult(response):
            req.BadRequestResult();
    }

    [OpenApiOperation(operationId: "ReadWorkloads", tags: new[] { "Workloads" })]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<WorkloadResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkloads")]
    public async Task<HttpResponseData> ReadWorkloads([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        IEnumerable<WorkloadResponse> response = await mediator.Send(new ReadWorkloadsQuery());

        return response is not null && response.Any() ? 
            req.OkObjectResult(response.ToArray()) : 
            req.NotFoundResult();
    }
        
    [OpenApiOperation(operationId: "ReadWorkload", tags: new[] { "Workloads" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(WorkloadFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkload")]
    public async Task<HttpResponseData> ReadWorkload([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return req.NotFoundResult();
        }

        WorkloadFullResponse? response = await mediator.Send(new ReadWorkloadQuery(id));
        
        return response is not null ? 
            req.OkObjectResult(response) : 
            req.NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadWorkloadsByPerson", tags: new[] { "Workloads" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<WorkloadFullResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkloadsByPerson")]
    public async Task<HttpResponseData> ReadWorkloadsByPerson([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return req.NotFoundResult();
        }

        IEnumerable<WorkloadFullResponse> response = await mediator.Send(new ReadWorkloadsByPersonQuery(id));
        
        return response is not null && response.Any() ?
            req.OkObjectResult(response) : 
            req.NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadWorkloadsByCustomer", tags: new[] { "Workloads" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<WorkloadFullResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadWorkloadsByCustomer")]
    public async Task<HttpResponseData> ReadWorkloadsByCustomer([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return req.NotFoundResult();
        }

        IEnumerable<WorkloadFullResponse>? response = await mediator.Send(new ReadWorkloadsByCustomerQuery(id));
        
        return response is not null && response.Any() ? 
            req.OkObjectResult(response) : 
            req.NotFoundResult();
    }

    [OpenApiOperation(operationId: "UpdateWorkload", tags: new[] { "Workloads" })]
    [OpenApiRequestBody("application/json", typeof(UpdateWorkloadCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("UpdateWorkload")]
    public async Task<HttpResponseData> UpdateWorkload([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        UpdateWorkloadCommand? request = await GetFromBody<UpdateWorkloadCommand>(req.Body);

        if (request is null)
        {
            return req.NotFoundResult();
        }

        WorkloadResponse? response = await mediator.Send(request);
        
        return response is not null?
            req.OkObjectResult(response) :
            req.NotFoundResult();
    }

    [OpenApiOperation(operationId: "DeleteWorkload", tags: new[] { "Workloads" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(WorkloadResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteWorkload")]
    public async Task<HttpResponseData> DeleteWorkload([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return req.NotFoundResult();
        }

        WorkloadResponse? response = await mediator.Send(new DeleteWorkloadCommand(id));
        
        return response is not null ? 
            req.OkObjectResult(response) : 
            req.NotFoundResult();
    }
}
