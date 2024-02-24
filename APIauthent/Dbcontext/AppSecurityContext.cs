using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIauthent.Dbcontext;

public class AppsecurityContext : IdentityDbContext<IdentityUser>
{ 
   public AppsecurityContext (DbContextOptions<AppsecurityContext> options)
   : base(options)
   {}
}