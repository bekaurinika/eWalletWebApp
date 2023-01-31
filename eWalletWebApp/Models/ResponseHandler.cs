using eWalletWebApp.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace eWalletWebApp.Models; 

public static class ResponseHandler {
    public static IActionResult GetExceptionResponse(this Controller controller, Exception ex) {
        if (ex is NotFoundException) {
            return controller.NotFound();
        }

        return controller.Problem();
    }

    public static ApiResponse GetAppResponse(object? contract) {
        var response = new ApiResponse {
            ResponseData = contract,
            Code = "200",
            Message = "Success"
        };
        return response;
    }
}