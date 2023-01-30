using System.ComponentModel.DataAnnotations;
using eWalletWebApp.EfCore;
using Newtonsoft.Json;

namespace eWalletWebApp.Models;

public class UserModel {
    public UserModel() {
        Accounts = new List<Account>();
    }

    public UserModel(User user, EWalletContext context) {
        this.UserId = user.UserId;
        this.Firstname = user.FirstName;
        this.Surname = user.Surname;
        this.Age = user.Age;
        this.PhoneNumber = user.PhoneNumber;
        this.Accounts = context.Accounts.Where(a => a.UserId == user.UserId).ToList();
    }
    public Guid? UserId { get; set; }
    public string? Firstname { get; set; }
    public string? Surname { get; set; }
    public int? Age { get; set; }
    public string? PhoneNumber { get; set; }
    public List<Account> Accounts { get; set; }
}