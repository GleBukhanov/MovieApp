using Microsoft.AspNetCore.Builder;

namespace MoviesApp.Middleware;

public static class ActorConrollerParamsExtensions
{
      public static IApplicationBuilder UseRequest(
                this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ActorControllerParams>();
            }
}