using Microsoft.EntityFrameworkCore;

namespace eWalletWebApp.EfCore;

public class EWalletContext : DbContext {
    public EWalletContext(DbContextOptions<EWalletContext> options) : base(options) {
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
}