using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiddlewareExample.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string WhiteIpAddress = "::1";

        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var reqIpAddress = context.Connection.RemoteIpAddress;

            bool AnyWhiteIpAdress = IPAddress.Parse(WhiteIpAddress).Equals(reqIpAddress);

            if (AnyWhiteIpAdress)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbidden");
            }
        }
    }
}
