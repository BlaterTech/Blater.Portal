using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Blater.Exceptions;
using Blater.Extensions;
using Blater.Interfaces;
using Blater.JsonUtilities;
using Blater.Models.User;
using Microsoft.JSInterop;

namespace Blater.Portal.Client.Services;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class AuthenticationService(
    ILocalStorageService localStorageService,
    IBlaterDatabaseStoreT<BlaterUser> store,
    BlaterUserToken blaterUserToken,
    NavigationService navigationService,
    ILogger<AuthenticationService> logger)
{
    private const string LocalStorageValueKey = "Blater-UserToken";
    
    //AuthStateChanged event
    public event Action? AuthStateChanged;

    public async Task Login(string jwtToken, bool saveStorage = true)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwtToken);
        
        var userTokenClaim = jwtTokenDecoded.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (userTokenClaim == null)
        {
            throw new Exception("Invalid jwt token, no UserToken claim found");
        }
        
        var userToken = userTokenClaim.Value.FromJson<BlaterUserToken>();
        
        if (userToken == null)
        {
            throw new Exception("Invalid jwt token, UserToken claim is not a valid UserToken");
        }

        var findUser = await store.FindOne(x => x.Id == userToken.UserId);
        if (findUser.HandleErrors(out var errors, out var response))
        {
            throw new BlaterException(errors);
        }
        
        if (response.LockoutEnabled)
        {
            return;
        }

        SetUserState(response);
        
        if (saveStorage)
        {
            await localStorageService.SetItemAsync(LocalStorageValueKey, jwtToken);
        }
        
        navigationService.Navigate($"home");
    }

    public async Task Logout()
    {
        var item= await localStorageService.GetItemAsync(LocalStorageValueKey);
        
        if (string.IsNullOrWhiteSpace(item))
        {
            await localStorageService.RemoveItemAsync(LocalStorageValueKey);
        }

        SetUserState(new BlaterUser());
    }
    
    public async Task TryAutoLogin()
    {
        var jwtToken = await localStorageService.GetItemAsync(LocalStorageValueKey);
        
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            navigationService.Navigate($"login");
            return;
        }
        
        await Login(jwtToken);
    }

    public async Task LoginBase64(string jwtTokenBase64, bool saveStorage = true)
    {
        var jwtToken = await jwtTokenBase64.FromBase64ToString();

        await Login(jwtToken, saveStorage);
        
        navigationService.Navigate("HomePage");
    }
    
    public void SetUserState(BlaterUser user)
    {
        blaterUserToken.UserId = user.Id;
        blaterUserToken.Email = user.Email;
        blaterUserToken.Name = user.Name;
        AuthStateChanged?.Invoke();
    }
}
