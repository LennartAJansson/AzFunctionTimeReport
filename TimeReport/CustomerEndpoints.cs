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

public sealed class CustomerEndpoints
{
    private readonly ILogger<CustomerEndpoints> logger;
    private readonly ITimeReportService dbService;

    public CustomerEndpoints(ILogger<CustomerEndpoints> logger, ITimeReportService dbService)
    {
        this.logger = logger;
        this.dbService = dbService;
    }

    [OpenApiOperation(operationId: "GetCustomers", tags: new[] { "Customers" }, Summary = "GetCustomers", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Customer))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetCustomers")]
    public async Task<IActionResult> GetCustomers(
        [HttpTrigger(AuthorizationLevel.Function, "get")]
        HttpRequestData req)
    {
        IEnumerable<Customer> customers = await dbService.GetCustomers();

        if (customers is not null && customers.Any())
        {
            return new OkObjectResult(customers.ToArray());
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "GetCustomer", tags: new[] { "Customers" }, Summary = "GetCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Customer))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetCustomer")]
    public async Task<IActionResult> GetCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Customer? customer = await dbService.GetCustomer(id);
            return customer is not null ? new OkObjectResult(customer) : new NotFoundResult();
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "CreateCustomer", tags: new[] { "Customers" }, Summary = "CreateCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Customer))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Customer))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Customer? customer = JsonSerializer.Deserialize<Customer>(req.Body, options);
            if (customer is not null)
            {
                await dbService.CreateCustomer(customer);
                return new OkObjectResult(customer);
            }
        }

        return new BadRequestResult();
    }


    [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" }, Summary = "UpdateCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Customer))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Customer))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Customer? customer = JsonSerializer.Deserialize<Customer>(req.Body, options);
            if (customer is not null)
            {
                await dbService.UpdateCustomer(customer);
                return new OkObjectResult(customer);
            }
        }

        return new BadRequestResult();
    }

    [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" }, Summary = "DeleteCustomer", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Customer))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Customer? customer = await dbService.DeleteCustomer(id);
            return customer is not null ? new OkObjectResult(customer) : new NotFoundResult();
        }

        return new NotFoundResult();
    }
}
