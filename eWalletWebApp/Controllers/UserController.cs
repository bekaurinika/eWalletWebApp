using eWalletWebApp.EfCore;
using eWalletWebApp.Exceptions;
using eWalletWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eWalletWebApp.Controllers;

[ApiController]
public class UserController : Controller {
    private readonly DbHelper _db;

    public UserController(EWalletContext context) {
        _db = new DbHelper(context);
    }

    // GET: /user
    [HttpGet("/users")]
    public IActionResult Get([FromQuery]UserModel userModel) {
        try {
            IEnumerable<UserModel> data = _db.GetUsers(userModel);
            return Ok(ResponseHandler.GetAppResponse(data));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }


    [HttpGet("/users/{id:guid}")]
    public IActionResult Get(Guid id) {
        try {
            if (!_db.TryGetUserById(id, out UserModel? data))
                throw new NotFoundException();
            
            return Ok(ResponseHandler.GetAppResponse(data));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }


    // POST: /user
    [HttpPost("/users")]
    public IActionResult Post([FromBody] UserModel user) {
        try {
            _db.CreateUser(user);
            return Ok(ResponseHandler.GetAppResponse(user));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }
    
    // PUT: /user/[id]
    [HttpPut("/users/{id}")]
    public IActionResult Put(Guid id, [FromBody] UserModel user) {
        try {
            _db.UpdateUser(id, user);
            return Ok(ResponseHandler.GetAppResponse(user));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }
    
    // DELETE: /user/[id]
    [HttpDelete("/users/{id}")]
    public IActionResult Delete(Guid id) {
        try {
            _db.DeleteUser(id);
            return Ok(ResponseHandler.GetAppResponse("Deleted"));
        }
        catch (Exception ex) {
            return ResponseHandler.GetExceptionResponse(this, ex);
        }
    }
}