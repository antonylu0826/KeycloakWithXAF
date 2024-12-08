using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.SystemModule;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace KeycloakWithXAF.Module.Services
{
    public class AdditionalLogonActionsCustomizationController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            AdditionalLogonActionsController additionalLogonActionsController = Frame.GetController<AdditionalLogonActionsController>();
            if (additionalLogonActionsController != null)
            {
                var action = additionalLogonActionsController.Actions.Where(action => action.Id == OpenIdConnectDefaults.AuthenticationScheme).FirstOrDefault();
                if (action != null)
                {
                    action.Caption = "KeyCloak";
                    action.ImageName = "Action_LogOnViaAzureAD";
                }
            }
        }
    }
}
