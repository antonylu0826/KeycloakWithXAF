using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using KeycloakWithXAF.WebApi.API.Security;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KeycloakWithXAF.WebApi.JWT;

[ApiController]
[Route("api/[controller]")]
// This is a JWT authentication service sample.
public class AuthenticationController : ControllerBase 
{
    readonly IAuthenticationTokenProvider tokenProvider;
    readonly KeycloakAuthService keycloakAuthService;
    public AuthenticationController(IAuthenticationTokenProvider tokenProvider, KeycloakAuthService keycloakAuthService = null)
    {
        this.tokenProvider = tokenProvider;
        this.keycloakAuthService = keycloakAuthService;
    }

    //[HttpPost("Authenticate")]
    //[SwaggerOperation("Checks if the user with the specified logon parameters exists in the database. If it does, authenticates this user.", "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>Authentication</a>.")]
    //public IActionResult Authenticate(
    //    [FromBody]
    //    [SwaggerRequestBody(@"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }")]
    //    AuthenticationStandardLogonParameters logonParameters
    //) {
    //    try {
    //        return Ok(tokenProvider.Authenticate(logonParameters));
    //    }
    //    catch(AuthenticationException ex) {
    //        return Unauthorized(ex.GetJson());
    //    }
    //}

    [HttpPost("KeycloakAuthenticate")]
    [SwaggerOperation("Checks if the user with the specified logon parameters exists in the database. If it does, authenticates this user.", "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>Authentication</a>.")]
    public async Task<IActionResult> KeycloakAuthenticate(
        [FromBody]
        [SwaggerRequestBody(@"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }")]
        AuthenticationStandardLogonParameters logonParameters
    )
    {
        try
        {
            var token = await keycloakAuthService.LoginAsync(logonParameters.UserName, logonParameters.Password);
            return Ok(token.AccessToken);
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized(ex.GetJson());
        }
    }
}
