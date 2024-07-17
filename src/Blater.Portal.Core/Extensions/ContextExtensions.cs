using System.Security.Claims;
using Blater.Extensions;
using Blater.Models.User;

namespace Blater.Portal.Core.Extensions;

public static class ContextExtensions
{
    public static BlaterUserToken GetUserAuthenticated(this ClaimsPrincipal principal)
    {
        var claims = principal.Claims
                              .GroupBy(x => x.Type)
                              .ToDictionary(
                                   g => g.Key.ToCamelCase(), 
                                   g => g
                                       .Select(c => c.Value)
                                       .ToList());

        var userToken = new BlaterUserToken();
        foreach (var prop in typeof(BlaterUserToken).GetProperties())
        {
            var key = prop.Name.ToCamelCase();
            if (!claims.TryGetValue(key, out var claimValues))
            {
                continue;
            }
            
            object value;

            if (prop.Name == "LockoutEnabled")
            {
                value = claimValues.First() == "enabled";
            }
            else if(prop.PropertyType == typeof(List<string>))
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