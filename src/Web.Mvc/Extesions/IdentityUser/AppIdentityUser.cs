using System.Security.Claims;

using IdentityModel;

using Microsoft.IdentityModel.JsonWebTokens;

using Web.Mvc.Domain.Base;

namespace Web.Mvc.Extesions.IdentityUser;

public class AppIdentityUser : IAppIdentityUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AppIdentityUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetLocalIpAddress()
    {
        return _contextAccessor.HttpContext?.Connection.LocalIpAddress?.ToString() ?? string.Empty;
    }

    public string GetRemoteIpAddress()
    {
        return _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }

    public Guid GetUserId()
    {
        if (!IsAuthenticated()) return Guid.Empty;

        var claim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claim))
            claim = _contextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        return claim is null ? Guid.Empty : Guid.Parse(claim);
    }

    public string GetUserName()
    {
        var username = _contextAccessor.HttpContext?.User.FindFirst("username")?.Value;
        if (!string.IsNullOrEmpty(username)) return username;

        username = _contextAccessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(username)) return username;

        username = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.Name)?.Value;
        if (!string.IsNullOrEmpty(username)) return username;

        username = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.GivenName)?.Value;
        if (!string.IsNullOrEmpty(username)) return username;

        var sub = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.Subject);
        if (sub != null) return sub.Value;

        return string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _contextAccessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }

    public bool IsInRole(string role)
    {
        return _contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User.IsInRole(role);
    }
}
