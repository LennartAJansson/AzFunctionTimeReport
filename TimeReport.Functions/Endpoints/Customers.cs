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

public sealed class Customers : BaseHttpFunction
{
    private readonly ILogger logger;
    private readonly IMediator mediator;

    public Customers(ILoggerFactory loggerFactory, IMediator mediator)
    {
        logger = loggerFactory.CreateLogger<Customers>();
        this.mediator = mediator;
    }

    [OpenApiOperation(operationId: "CreateCustomer", tags: new[] { "Customers" })]
    [OpenApiRequestBody("application/json", typeof(CreateCustomerCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        CreateCustomerCommand? request = await GetFromBody<CreateCustomerCommand>(req.Body);
        if (request is null)
        {
            return new BadRequestResult();
        }

        CustomerResponse response = await mediator.Send(request);

        return response is not null ?
            new OkObjectResult(response) : 
            new BadRequestResult();
    }

    [OpenApiOperation(operationId: "ReadCustomers", tags: new[] { "Customers" })]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<CustomerResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadCustomers")]
    public async Task<IActionResult> ReadCustomers([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        IEnumerable<CustomerResponse>? response = await mediator.Send(new ReadCustomersQuery());

        return response is not null && response.Any() ?
            new OkObjectResult(response.ToArray()) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadCustomer", tags: new[] { "Customers" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadCustomer")]
    public async Task<IActionResult> ReadCustomer([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        CustomerFullResponse? response = await mediator.Send(new ReadCustomerQuery(id));

        return response is not null ?
            new OkObjectResult(response) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" })]
    [OpenApiRequestBody("application/json", typeof(UpdateCustomerCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        UpdateCustomerCommand? request = await GetFromBody<UpdateCustomerCommand>(req.Body);

        if (request is null)
        {
            return new NotFoundResult();
        }

        CustomerResponse response = await mediator.Send(request);

        return response is not null?
            new OkObjectResult(response) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        CustomerResponse? response = await mediator.Send(new DeleteCustomerCommand(id));

        return response is not null ?
            new OkObjectResult(response) :
            new NotFoundResult();
    }
}
