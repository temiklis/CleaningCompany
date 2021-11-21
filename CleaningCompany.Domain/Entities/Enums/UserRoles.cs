using System.ComponentModel;

namespace CleaningCompany.Domain.Entities.Enums
{
    public enum UserRoles
    {
        [Description("Admin")]
        Admin,
        [Description("Employee")]
        Employee,
        [Description("User")]
        User
    }
}
