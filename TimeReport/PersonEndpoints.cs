namespace TimeReport;

using System.Net;
using System.Text.Json;

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

public sealed class PersonEndpoints
{
    private readonly ILogger<PersonEndpoints> logger;
    private readonly ITimeReportService dbService;

    public PersonEndpoints(ILogger<PersonEndpoints> logger, ITimeReportService dbService)
    {
        this.logger = logger;
        this.dbService = dbService;
    }

    [OpenApiOperation(operationId: "GetPeople", tags: new[] { "People" }, Summary = "GetPeople", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Person))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetPeople")]
    public async Task<IActionResult> GetPeople(
        [HttpTrigger(AuthorizationLevel.Function, "get")]
        HttpRequestData req)
    {
        IEnumerable<Person> people = await dbService.GetPeople();

        if (people is not null && people.Any())
        {
            return new OkObjectResult(people.ToArray());
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "GetPerson", tags: new[] { "People" }, Summary = "GetPerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Person))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("GetPerson")]
    public async Task<IActionResult> GetPerson(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Person? person = await dbService.GetPerson(id);
            return person is not null ? new OkObjectResult(person) : new NotFoundResult();
        }

        return new NotFoundResult();
    }

    [OpenApiOperation(operationId: "CreatePerson", tags: new[] { "People" }, Summary = "CreatePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Person))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Person))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreatePerson")]
    public async Task<IActionResult> CreatePerson(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Person? person = JsonSerializer.Deserialize<Person>(req.Body, options);
            if (person is not null)
            {
                await dbService.CreatePerson(person);
                return new OkObjectResult(person);
            }
        }

        return new BadRequestResult();
    }


    [OpenApiOperation(operationId: "UpdatePerson", tags: new[] { "People" }, Summary = "UpdatePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Person))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Person))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("UpdatePerson")]
    public async Task<IActionResult> UpdatePerson(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Person? person = JsonSerializer.Deserialize<Person>(req.Body, options);
            if (person is not null)
            {
                await dbService.UpdatePerson(person);
                return new OkObjectResult(person);
            }
        }

        return new BadRequestResult();
    }

    [OpenApiOperation(operationId: "DeletePerson", tags: new[] { "People" }, Summary = "DeletePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Person))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeletePerson")]
    public async Task<IActionResult> DeletePerson(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (int.TryParse(req.Query("id"), out int id))
        {
            Person? person = await dbService.DeletePerson(id);
            return person is not null ? new OkObjectResult(person) : new NotFoundResult();
        }

        return new NotFoundResult();
    }
}
