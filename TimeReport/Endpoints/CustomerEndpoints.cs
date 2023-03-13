namespace TimeReport.Endpoints;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

public sealed class CustomerEndpoints : BaseHttpFunction
{
    private readonly ILogger<CustomerEndpoints> logger;
    private readonly IMediator mediator;

    public CustomerEndpoints(ILogger<CustomerEndpoints> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [OpenApiOperation(operationId: "CreateCustomer", tags: new[] { "Customers" }, Summary = "CreateCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(CreateCustomerCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        CreateCustomerCommand? request = await GetFromBody<CreateCustomerCommand>(req.Body);

        if (request is null)
        {
            return new BadRequestResult();
        }

        CustomerResponse response = await mediator.Send(request);
        return new OkObjectResult(response);
    }

    [OpenApiOperation(operationId: "ReadCustomers", tags: new[] { "Customers" }, Summary = "ReadCustomers", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<CustomerResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadCustomers")]
    public async Task<IActionResult> ReadCustomers(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        IEnumerable<CustomerResponse> response = await mediator.Send(new ReadCustomersQuery());

        return response is not null && response.Any() ? new OkObjectResult(response.ToArray()) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadCustomer", tags: new[] { "Customers" }, Summary = "ReadCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadCustomer")]
    public async Task<IActionResult> ReadCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        CustomerFullResponse? response = await mediator.Send(new ReadCustomerQuery(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" }, Summary = "UpdateCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(UpdateCustomerCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        UpdateCustomerCommand? request = await GetFromBody<UpdateCustomerCommand>(req.Body);

        if (request is null)
        {
            return new NotFoundResult();
        }

        CustomerResponse response = await mediator.Send(request);
        return new OkObjectResult(response);
    }

    [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" }, Summary = "DeleteCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiParameter(name: "value", In = ParameterLocation.Query, Required = true, Type = typeof(bool))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomerResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id) ||
            !bool.TryParse(req.Query("value"), out _))
        {
            return new NotFoundResult();
        }

        bool value = false;
        _ = !value;

        CustomerResponse? response = await mediator.Send(new DeleteCustomerCommand(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }

}
