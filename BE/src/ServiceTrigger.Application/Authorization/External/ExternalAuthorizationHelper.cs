using Abp.Authorization;
using Abp.Extensions;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceTrigger.Authorization.Users;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ServiceTrigger.Authorization.External
{
     public static class ExternalAuthorizationHelper
    {
        private static ConcurrentDictionary<string, DateTime> _dict { get; set; } = new ConcurrentDictionary<string, DateTime>();

        public static bool IsGranted(HttpContext context, string authenticateApiUrl,string permissionName)
        {
            var isGranted = false;

            try
            {
                var token = context.Request.Cookies["Abp.AuthToken"];

                long userId = GetUserIdFromJwtToken(token);

                var userManager = context.RequestServices.GetRequiredService<UserManager>();

                isGranted = userManager.IsGrantedAsync(userId, permissionName).Result;
            }
            catch (Exception ex)
            {
            }

            return isGranted;
        }

        private static long GetUserIdFromJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // http://schemas.microsoft.com/ws/2008/06/identity/claims/role

            var sub = jsonToken.Claims.First(claim => claim.Type == "sub").Value;

            var userId = long.Parse(sub);

            return userId;
        }
    }
}
