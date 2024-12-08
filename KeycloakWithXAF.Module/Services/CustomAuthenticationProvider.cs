using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using KeycloakWithXAF.Module.BusinessObjects;
using System.Security.Claims;
using System.Security.Principal;

namespace KeycloakWithXAF.Module.Services
{
    public class CustomAuthenticationProvider : IAuthenticationProviderV2
    {
        private readonly UserManager userManager;

        public CustomAuthenticationProvider(UserManager userManager)
        {
            this.userManager = userManager;
        }

        public object Authenticate(IObjectSpace objectSpace)
        {
            var currentPrincipal = userManager.GetCurrentPrincipal();
            return Authenticate(objectSpace, currentPrincipal);
        }

        public object Authenticate(IObjectSpace objectSpace, IPrincipal currentPrincipal)
        {
            if (currentPrincipal?.Identity?.IsAuthenticated ?? false)
            {
                var user = userManager.FindUserByPrincipal<ApplicationUser>(objectSpace, currentPrincipal);
                if (user != null)
                {
                    return new UserResult<ApplicationUser>(user);
                }
                //Find user from web api KeycloakAuthService
                var claimUserName = ((ClaimsPrincipal)currentPrincipal).FindFirst("preferred_username")?.Value;
                if (!string.IsNullOrEmpty(claimUserName))
                {
                    var findUsers = objectSpace.GetObjects<ApplicationUser>(DevExpress.Data.Filtering.CriteriaOperator.Parse("UserName =?", claimUserName));
                    if (findUsers.Count >= 1)
                    {
                        return new UserResult<ApplicationUser>(findUsers.First());
                    }
                }

                // The code below creates users for testing purposes only.
#if !RELEASE
                bool autoCreateUser = true;
                if (autoCreateUser)
                {
                    UserResult<ApplicationUser> userResult;
                    //Keycloak 
                    if (currentPrincipal.Identity.Name is null)
                    {
                        var claimsPrincipal = currentPrincipal as ClaimsPrincipal;
                        var userIdClaim = claimsPrincipal.FindFirst("sub") ?? claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Unknown user id");
                        var providerUserKey = userIdClaim.Value;
                        var userName = claimsPrincipal.FindFirst("preferred_username")?.Value; //claimsPrincipal.Identity.Name;
                        var loginProviderName = claimsPrincipal.Identity.AuthenticationType;
                        userResult = userManager.CreateUser<ApplicationUser>(objectSpace, userName, loginProviderName, providerUserKey, user =>
                        {
                            user.Roles.Add(objectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default"));
                        });
                    }
                    else
                    {
                        userResult = userManager.CreateUser<ApplicationUser>(objectSpace, currentPrincipal, user => {
                            user.Roles.Add(objectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default"));
                        });
                    }

                    if (!userResult.Succeeded)
                    {
                        //throw userResult.Error;
                    }
                    return userResult;
                }
#endif
            }

            return null;
        }


    }
}
