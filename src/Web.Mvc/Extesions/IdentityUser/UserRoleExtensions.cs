using static Web.Mvc.Domain.Enums.EnumAccessPriority;

namespace Web.Mvc.Extesions.IdentityUser
{
    public static class UserRoleExtensions
    {
        public static bool HasRole(this UserRole userRole, UserRole role)
        {
            return (userRole & role) == role;
        }
    }
}
