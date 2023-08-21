namespace TimeReport.Functions.Endpoints;
using System.Net;
using System.Text.Json;

using Microsoft.Azure.Functions.Worker.Http;

public static class HttpResponses
{
    public static HttpResponseData BadRequestResult(this HttpRequestData req)
    {
        HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);

        return response;
    }

    public static HttpResponseData NotFoundResult(this HttpRequestData req)
    {
        HttpResponseData response = req.CreateResponse(HttpStatusCode.NotFound);

        return response;
    }

    public static HttpResponseData OkObjectResult<T>(this HttpRequestData req, T data)
    {
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        string json = JsonSerializer.Serialize(data, options);

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        response.WriteString(json);

        return response;
    }
}
