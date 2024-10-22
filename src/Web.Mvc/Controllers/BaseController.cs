using Microsoft.AspNetCore.Mvc;

using Web.Mvc.Domain.Base;

namespace Web.Mvc.Controllers;
public abstract class BaseController : Controller
{
    protected Guid UserId { get; set; }
    protected string UserName { get; set; } = string.Empty; 
    protected bool UserIsAuthenticated;

    protected BaseController(IAppIdentityUser user)
    {
        UserIsAuthenticated = user.IsAuthenticated();

        if (UserIsAuthenticated)
        {
            UserId = user.GetUserId();
            UserName= user.GetUserName();
        }
    }



}
