using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blater.SDK.Contracts.Authentication.Request;
using Blater.SDK.Contracts.Common.Request;
using Blater.SDK.Interfaces.BlaterAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Components.Account.Base;

public class BaseAccount : ComponentBase
{
    #region Props

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    [Inject]
    private IBlaterAuthLoginStoreEndpoints LoginStore { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; } = default!;
    
    protected ModelAccountLogin InputLogin { get; set; } = new();
    
    protected ModelAccountRegister InputRegister { get; set; } = new();
    
    protected bool Success { get; set; }
    protected MudForm Form { get; set; }

    #endregion

    #region Methods
    
    //TODO: Criar componente para utilizar os erros
    
    public async Task OnValidRegisterSubmit()
    {
        var result = await LoginStore.Register(new RegisterBlaterUserRequest
        {
            Email = InputRegister.Email,
            Name = InputRegister.Name,
            Password = InputRegister.Password
        });

        if (result.HandleErrors(out var errors, out var user))
        {
            return;
        }

        InputLogin = new ModelAccountLogin
        {
            Password = InputRegister.Password,
            Email = user.Email
        };

        //await OnValidLoginSubmit();
    }

    public async Task OnValidLoginSubmit()
    {
        var result = await LoginStore.LoginLocal(new AuthRequest
        {
            Email = InputLogin.Email,
            Password = InputLogin.Password
        });

        if (result.HandleErrors(out var errors, out var jwt))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(jwt))
        {
            return;
        }

        await ReadJwtAndRedirectTo(jwt);
    }

    private async Task ReadJwtAndRedirectTo(string jwt)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwt);

        if (jwtTokenDecoded.ValidTo.ToUniversalTime() <= DateTime.UtcNow)
        {
            return;
        }

        var claims = jwtTokenDecoded.Claims;
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        RedirectManager.RedirectTo("home");
    }

    #endregion

    #region Models

    protected sealed class ModelAccountRegister
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = "";
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
    
    protected sealed class ModelAccountLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    #endregion
}