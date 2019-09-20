using System;
using Microsoft.AspNetCore.Routing;

namespace BOS.Webclient.Infrastructure.Services
{
    public class LoggedInRequest : LoggedInRequestBase
    {
        public static LoggedInRequest For()
        {
            return new LoggedInRequest();
        }
    }

    public class LoggedInRequestBase
    {
        public Guid UserId { get; set; }
    }

    public static class LoggedInRequestExtensions
    {
        public static T Populate<T>(this T self, RouteData routeData)
            where T : LoggedInRequestBase
        {
            self.UserId = (Guid) routeData.Values["userId"];
            return self;
        }
    }
}