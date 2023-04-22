using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PBL3.Models.Domain
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {}
        
        // Thêm những bảng khác, Dbset<> sau này ở bên dưới
    }
}
