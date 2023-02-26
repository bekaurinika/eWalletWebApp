using System.ComponentModel;
using eWalletWebApp.EfCore;
using eWalletWebApp.Exceptions;

namespace eWalletWebApp.Models;

public class DbHelper {
    private readonly EWalletContext _context;

    public DbHelper(EWalletContext context) {
        _context = context;
    }

    public List<UserModel> GetUsers(UserModel userModel) {
        var response = new List<UserModel>();
        var dataList = _context.Users.ToList();

        dataList.Where(FilterUser).ToList().ForEach(row => response.Add(new UserModel(row, _context)));
        if (response.Count == 0)
            throw new NotFoundException();

        return response;

        bool FilterUser(User user) {
            bool result = true;

            if (!String.IsNullOrEmpty(userModel.Firstname))
                result &= user.FirstName == userModel.Firstname;
            if (!String.IsNullOrEmpty(userModel.Surname))
                result &= user.Surname == userModel.Surname;
            if (userModel.Age is not null)
                result &= user.Age == userModel.Age;
            if (!String.IsNullOrEmpty(userModel.PhoneNumber))
                result &= user.PhoneNumber == userModel.PhoneNumber;

            return result;
        }
    }

    public bool TryGetUserById(Guid id, out UserModel? userModel) {
        userModel = null;
        var row = _context.Users.FirstOrDefault(d => d.UserId.Equals(id));
        if (row is not null)
            userModel = new UserModel(row, _context);

        return row is not null;
    }

    public void CreateUser(UserModel model) {
        var user = new User() {
            UserId = Guid.NewGuid(),
            FirstName = model.Firstname,
            Surname = model.Surname,
            Age = model.Age,
            PhoneNumber = model.PhoneNumber,
            Accounts = model.Accounts
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(Guid id, UserModel model) {
        if (!TryGetUserById(id, out _))
            throw new NotFoundException();

        var user = _context.Users.FirstOrDefault(d => d.UserId.Equals(id));
        user.FirstName = model.Firstname ?? user.FirstName;
        user.Surname = model.Surname ?? user.Surname;
        user.Age = model.Age ?? user.Age;
        user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;

        _context.SaveChanges();
    }

    public void DeleteUser(Guid id) {
        var user = _context.Users.FirstOrDefault(d => d.UserId.Equals(id));

        if (user is null)
            throw new NotFoundException();

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public List<AccountModel> GetAccounts(AccountModel accountModel, UserModel userModel) {
        var datalist = _context.Accounts.ToList();
        var response = datalist.Select(account => new AccountModel(account)).ToList();
        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(accountModel)) {
            string name = descriptor.Name;
            object? value = descriptor.GetValue(accountModel);
            if (value is not null) {
                response = response.Where(d => descriptor.GetValue(d).Equals(value)).ToList();
            }
        }

        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(userModel)) {
            if (descriptor.Name.Contains("Account")) continue;

            string name = descriptor.Name;
            object? value = descriptor.GetValue(userModel);
            if (value is not null) {
                response = response.Where(d => {
                    TryGetUserById(d.UserId.Value, out UserModel outUserModel);
                    return descriptor.GetValue(outUserModel).Equals(value);
                }).ToList();
            }
        }

        if (response.Count == 0)
            throw new NotFoundException();

        return response;
    }

    public AccountModel GetAccountModelById(Guid id) => new AccountModel(GetAccountById(id));

    public Account GetAccountById(Guid id) {
        var account = _context.Accounts.FirstOrDefault(d => d.AccountId.Equals(id));
        if (account is null)
            throw new NotFoundException();

        return account;
    }

    public void CreateAccount(AccountModel model) {
        if (!TryGetUserById(model.UserId.Value, out _))
            throw new NotFoundException();

        var account = new Account() {
            AccountId = Guid.NewGuid(),
            Name = model.Name,
            Balance = 0,
            Currency = model.Currency,
            UserId = model.UserId
        };

        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void UpdateAccount(Guid id, AccountModel model) {
        var account = GetAccountById(id);
        account.Name = model.Name ?? account.Name;
        account.Currency = model.Currency ?? account.Currency;

        _context.SaveChanges();
    }

    public void DeleteAccount(Guid id) {
        var account = GetAccountById(id);

        _context.Accounts.Remove(account);
        _context.SaveChanges();
    }

    public void AddFunds(Guid id, decimal amount) {
        var account = GetAccountById(id);
        account.Balance += amount;

        _context.SaveChanges();
    }

    public void RemoveFunds(Guid id, decimal amount) {
        var account = GetAccountById(id);
        account.Balance -= amount;

        _context.SaveChanges();
    }
}
