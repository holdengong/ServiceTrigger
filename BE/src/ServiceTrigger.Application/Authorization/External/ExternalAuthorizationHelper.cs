using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
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

        public static bool IsGranted(HttpContext httpContext, string authenticateApiUrl,string permissionName)
        {
            var isGranted = false;

            try
            {
                var token = httpContext.Request.Cookies["Abp.AuthToken"];

                long userId = GetUserIdFromJwtToken(token);

                string url = authenticateApiUrl + $"?userId={userId}&permissionName={permissionName}";

                if (url.StartsWith("https"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }

                HttpClient hc = new HttpClient();

                var postData = new
                {
                    userId = userId,
                    permissionName = permissionName
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(postData));

                var result = hc.PostAsync(url, stringContent).Result.Content.ReadAsStringAsync().Result;

                isGranted = result.Equals(true.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }
            catch (Exception ex)
            {
            }

            return isGranted;
        }

        public static long GetUserIdFromJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // http://schemas.microsoft.com/ws/2008/06/identity/claims/role

            var sub = jsonToken.Claims.First(claim => claim.Type == "sub").Value;

            var userId = long.Parse(sub);

            return userId;
        }

        public static bool GetFromCache(string userIdAndPermissionName)
        {
            bool isGranted = false;
            if (_dict.ContainsKey(userIdAndPermissionName))
            {
                if (DateTime.Now - _dict[userIdAndPermissionName] > TimeSpan.FromMinutes(1))
                {
                    _dict.TryRemove(userIdAndPermissionName, out DateTime dt);
                }
                else
                {
                    isGranted = true;
                }
            }

            return isGranted;
        }
    }
}
