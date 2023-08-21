namespace TimeReport.Functions.Endpoints;
using System.Text.Json;

public class BaseHttpFunction
{
    public Task<T?> GetFromBody<T>(Stream body) where T : class
    {
        if (body is not null && body.Length > 0)
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            T? request = JsonSerializer.Deserialize<T>(body, options);
            return Task.FromResult(request);
        }

        return Task.FromResult<T?>(null);
    }
}
