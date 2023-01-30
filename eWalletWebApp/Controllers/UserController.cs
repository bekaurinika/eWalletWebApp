using eWalletWebApp.EfCore;
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
        ResponseType type = ResponseType.Success;
        try {
            IEnumerable<UserModel> data = _db.GetUsers(userModel);
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


    [HttpGet("/users/{id:guid}")]
    public IActionResult Get(Guid id) {
        ResponseType type = ResponseType.NotFound;
        try {
            if (_db.TryGetUserById(id, out UserModel? data)) 
                type = ResponseType.Success;
            
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex) {
            type = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }


    // POST: /user
    [HttpPost("/users")]
    public IActionResult Post([FromBody] UserModel user) {
        ResponseType type = ResponseType.Success;
        try {
            _db.CreateUser(user);
            return Ok(ResponseHandler.GetAppResponse(type, user));
        }
        catch (Exception ex) {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    
    // PUT: /user/[id]
    [HttpPut("/users/{id}")]
    public IActionResult Put(Guid id, [FromBody] UserModel user) {
        ResponseType type = ResponseType.Success;
        try {
            _db.UpdateUser(id, user);
            return Ok(ResponseHandler.GetAppResponse(type, user));
        }
        catch (Exception ex) {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    
    // DELETE: /user/[id]
    [HttpDelete("/users/{id}")]
    public IActionResult Delete(Guid id) {
        ResponseType type = ResponseType.Success;
        try {
            _db.DeleteUser(id);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted"));
        }
        catch (Exception ex) {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}