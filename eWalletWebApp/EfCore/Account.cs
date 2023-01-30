using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eWalletWebApp.EfCore;

[Table("Accounts")]
public class Account {
    [Key] [Required] 
    public Guid AccountId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Currency { get; set; }
    [Required] 
    public decimal Balance { get; set; } = 0;
    [Required] [ForeignKey("User")]
    public Guid? UserId { get; set; }
}