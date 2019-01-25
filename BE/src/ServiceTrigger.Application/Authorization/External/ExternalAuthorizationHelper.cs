using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
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
        public static Dictionary<string, DateTime> SimpleCache { get; set; } = new Dictionary<string, DateTime>();
        public static bool IsGranted(HttpContext httpContext, string authenticateApiUrl,string permissionName)
        {
            try
            {
                var token = httpContext.Request.Cookies["Abp.AuthToken"];

                long userId = GetUserIdFromJwtToken(token);

                var isGranted = false;

                string cacheKey = $"{userId}_{permissionName}";
                isGranted = GetFromCache(cacheKey);

                if (!isGranted)
                {
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

                    if (isGranted)
                    {
                        SimpleCache.Add(cacheKey, DateTime.Now);
                    }
                }

                return isGranted;
            }
            catch (Exception ex)
            {
                return false;
            }
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
            if (SimpleCache.ContainsKey(userIdAndPermissionName))
            {
                if (DateTime.Now - SimpleCache[userIdAndPermissionName] > TimeSpan.FromMinutes(1))
                {
                    SimpleCache.Remove(userIdAndPermissionName);
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
