using Microsoft.AspNetCore.Identity;

namespace CoreHRM.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true; // 使用者「啟用/禁用」的狀態
    }
}
