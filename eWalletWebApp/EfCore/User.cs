using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eWalletWebApp.EfCore;

[Table("Users")]
public class User {
    public User() {
        Accounts = new List<Account>();
    }

    [Key] [Required]
    public Guid UserId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public int? Age { get; set; }
    public string? PhoneNumber { get; set; }
    [NotMapped]
    public List<Account> Accounts { get; set; }
}