using eWalletWebApp.EfCore;
using eWalletWebApp.Exceptions;
using eWalletWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eWalletWebApp.Controllers;

public class AccountController : Controller {
    private readonly DbHelper _db;

    public AccountController(EWalletContext context) {
        _db = new DbHelper(context);
    }

    [HttpGet("/accounts")]
    public IActionResult Get([FromQuery] AccountModel accountModel, [FromQuery] UserModel userModel) {
        try {
            IEnumerable<AccountModel> data = _db.GetAccounts(accountModel, userModel);

            return Ok(ResponseHandler.GetAppResponse(data));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpGet("/accounts/{id:guid}")]
    public IActionResult Get(Guid id) {
        try {
            var account = _db.GetAccountModelById(id);

            return Ok(ResponseHandler.GetAppResponse(account));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpPost("/accounts")]
    public IActionResult Post([FromBody] AccountModel accountModel) {
        try {
            _db.CreateAccount(accountModel);
            return Ok(ResponseHandler.GetAppResponse(accountModel));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpPut("/accounts/{id:guid}")]
    public IActionResult Put(Guid id, [FromBody] AccountModel accountModel) {
        try {
            _db.UpdateAccount(id, accountModel);
            return Ok(ResponseHandler.GetAppResponse(accountModel));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpDelete("/accounts/{id:guid}")]
    public IActionResult Delete(Guid id) {
        try {
            _db.DeleteAccount(id);
            return Ok(ResponseHandler.GetAppResponse("Deleted"));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpPost("/accounts/{id:guid}/add-funds")]
    public IActionResult AddFunds(Guid id, decimal amount) {
        try {
            _db.AddFunds(id, amount);

            return Ok(ResponseHandler.GetAppResponse("Added: " + amount));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }

    [HttpPost("/accounts/{id:guid}/remove-funds")]
    public IActionResult RemoveFunds(Guid id, decimal amount) {
        try {
            _db.RemoveFunds(id, amount);

            return Ok(ResponseHandler.GetAppResponse("Removed: " + amount));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }
}