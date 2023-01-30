using eWalletWebApp.EfCore;
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
        ResponseType type = ResponseType.Success;
        try {
            IEnumerable<AccountModel> data = _db.GetAccounts(accountModel, userModel);
            if (!data.Any()) {
                type = ResponseType.NotFound;
            }

            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    [HttpGet("/accounts/{id:guid}")]
    public IActionResult Get(Guid id) {
        ResponseType type = ResponseType.NotFound;
        try {
            if (_db.TryGetAccountById(id, out AccountModel? data))
                type = ResponseType.Success;

            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    [HttpPost("/accounts")]
    public IActionResult Post([FromBody] AccountModel accountModel) {
        ResponseType type = ResponseType.Success;
        try {
            _db.CreateAccount(accountModel);
            return Ok(ResponseHandler.GetAppResponse(type, accountModel));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    //TODO: compare this to users 
    [HttpPut("/accounts/{id:guid}")]
    public IActionResult Put(Guid id, [FromBody] AccountModel accountModel) {
        ResponseType type = ResponseType.Success;
        try {
            if (_db.TryGetAccountById(id, out AccountModel? data)) {
                _db.UpdateAccount(id, accountModel);
            }
            else {
                type = ResponseType.NotFound;
            }

            return Ok(ResponseHandler.GetAppResponse(type, accountModel));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    [HttpDelete("/accounts/{id:guid}")]
    public IActionResult Delete(Guid id) {
        ResponseType type = ResponseType.Success;
        try {
            _db.DeleteAccount(id);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted"));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    
    [HttpPost("/accounts/{id:guid}/add-funds")]
    public IActionResult AddFunds(Guid id, decimal amount) {
        ResponseType type = ResponseType.Success;
        try {
            if (_db.TryGetAccountById(id, out AccountModel? data)) {
                _db.AddFunds(id, amount);
            }
            else {
                type = ResponseType.NotFound;
            }

            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    
    [HttpPost("/accounts/{id:guid}/remove-funds")]
    public IActionResult RemoveFunds(Guid id, decimal amount) {
        ResponseType type = ResponseType.Success;
        try {
            if (_db.TryGetAccountById(id, out AccountModel? data)) {
                _db.RemoveFunds(id, amount);
            }
            else {
                type = ResponseType.NotFound;
            }

            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}