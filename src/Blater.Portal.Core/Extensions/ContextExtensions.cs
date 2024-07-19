using System.Security.Claims;
using Blater.Extensions;
using Blater.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blater.Portal.Core.Extensions;

public static class ContextExtensions
{
    public static ClaimsPrincipal GetClaimsPrincipal(this BlaterUserToken userToken)
    {
        var claims = new List<Claim>();

        foreach (var prop in typeof(BlaterUserToken).GetProperties())
        {
            var key = prop.Name;
            var value = prop.GetValue(userToken);

            if (value == null)
            {
                continue;
            }

            if (prop.Name == "LockoutEnabled")
            {
                var claimValue = (bool)value ? "enabled" : "disabled";
                claims.Add(new Claim(key, claimValue));
            }
            else if (prop.PropertyType == typeof(List<string>))
            {
                var values = (List<string>)value;
                claims.AddRange(values.Select(val => new Claim(key, val)));
            }
            else
            {
                claims.Add(new Claim(key, value.ToString() ?? string.Empty));
            }
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return principal;
    }

    public static BlaterUserToken GetUserAuthenticated(this ClaimsPrincipal principal)
    {
        var claims = principal
                    .Claims
                    .GroupBy(x => x.Type)
                    .ToDictionary(
                         g => g.Key,
                         g => g.Select(c => c.Value).ToList()
                     );
        
        var userToken = new BlaterUserToken();
        foreach (var prop in typeof(BlaterUserToken).GetProperties())
        {
            if (!claims.TryGetValue(prop.Name, out var claimValues))
            {
                continue;
            }

            object value;

            if (prop.Name == "LockoutEnabled")
            {
                value = claimValues.First() == "enabled";
            }
            else if (prop.PropertyType == typeof(List<string>))
            {
                value = claimValues;
            }
            else
            {
                value = claimValues.First();
            }

            prop.SetValue(userToken, value);
        }

        return userToken;
    }
}