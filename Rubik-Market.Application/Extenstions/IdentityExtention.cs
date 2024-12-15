using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Application.Extenstions
{
    public static class IdentityExtention
    {
        public static string GetUserFullName(this ClaimsPrincipal claimsPrincipal)
        {
            var userFullName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == "UserFullName")?.Value;
            return userFullName;
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return default;
            }

            return int.Parse(userId);
        }
    }
}
