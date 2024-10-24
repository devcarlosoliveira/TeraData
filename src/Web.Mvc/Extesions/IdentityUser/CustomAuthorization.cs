﻿using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Mvc.Extesions.IdentityUser;

public class CustomAuthorization
{
    public static bool ValidadeUserClaims(HttpContext context, string claimName, string claimValue)
    {
        if (context.User.Identity == null) throw new InvalidOperationException();

        return context.User.Identity.IsAuthenticated &&
            context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
    }
}
public class ClaimFilterRequirement : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimFilterRequirement(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
        }

        if (!CustomAuthorization.ValidadeUserClaims(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }

    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequirement))
    {
        Arguments = [new Claim(claimName, claimValue)];
    }
}

