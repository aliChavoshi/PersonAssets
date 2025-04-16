namespace PersonAssets.Middlewares;

public class CustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //before logic
        await context.Response.WriteAsync("start before logic CustomMiddleware");
        await next(context); //end point
        //after logic
        await context.Response.WriteAsync("start after logic CustomMiddleware");
    }
}

public static class MyCustomMiddlewareExtension
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}