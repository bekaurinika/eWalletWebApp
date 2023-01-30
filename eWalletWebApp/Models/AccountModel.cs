using eWalletWebApp.EfCore;

namespace eWalletWebApp.Models;

public class AccountModel {
    public AccountModel() {
        
    }
    public AccountModel(Account row) {
        
        this.AccountId = row.AccountId;
        this.Name = row.Name;
        this.Currency = row.Currency;
        this.Balance = row.Balance;
        this.UserId = row.UserId;
    }

    public Guid? AccountId { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public decimal? Balance { get; set; }
    public Guid? UserId { get; set; }
}