using Microsoft.AspNetCore.Builder;

namespace StudentsAPI.Middleware
{
    public static class LicensingMiddlewareExtension
    {
        public static IApplicationBuilder UseLicensing(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LicensingMiddleware>();
        }
    }
}
