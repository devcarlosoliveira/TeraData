namespace Web.Mvc.Domain.Base;

public interface IAppIdentityUser
{
    Guid GetUserId();
    string GetUserName();
    bool IsAuthenticated();
    bool IsInRole(string role);
    string GetRemoteIpAddress();
    string GetLocalIpAddress();
}
