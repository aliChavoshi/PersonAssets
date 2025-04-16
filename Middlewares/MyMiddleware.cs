namespace PersonAssets.Middlewares;

public class MyMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        //before logic
        await context.Response.WriteAsync("start before logic MyMiddleware");
        await next(context); //end point
        //after logic
        await context.Response.WriteAsync("start after logic MyMiddleware");
    }
}

public static class MyMiddlewareExtension
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyMiddleware>();
    }
}