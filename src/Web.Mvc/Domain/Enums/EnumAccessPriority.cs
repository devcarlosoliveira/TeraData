namespace Web.Mvc.Domain.Enums
{
    public class EnumAccessPriority
    {
        [Flags]
        public enum UserRole
        {
            None = 0,
            Admin = 1 << 0,      // 1
            User = 1 << 1,       // 2
            Moderator = 1 << 2,  // 4
            Guest = 1 << 3,      // 8
            SuperAdmin = Admin | Moderator // 5
        }
    }
}
