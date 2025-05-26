using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Hook vào pipeline
        var originalBodyStream = context.Response.Body;

        using var memStream = new MemoryStream();
        context.Response.Body = memStream;

        await _next(context);

        if (context.Response.StatusCode == 400 && context.Items["__HandledByFluentValidation"] == null)
        {
            memStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(memStream);
            var body = await reader.ReadToEndAsync();

            if (body.Contains("One or more validation errors occurred."))
            {
                // Parse mặc định ProblemDetails
                var parsed = JsonSerializer.Deserialize<ValidationProblemDetails>(body, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var custom = new
                {
                    message = "Dữ liệu không hợp lệ.",
                    errors = parsed?.Errors.SelectMany(kv => kv.Value.Select(msg => new
                    {
                        field = kv.Key,
                        message = msg
                    }))
                };

                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength = null;

                var json = JsonSerializer.Serialize(custom);
                await context.Response.WriteAsync(json);
                return;
            }

            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBodyStream);
        }
        else
        {
            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBodyStream);
        }
    }
}
