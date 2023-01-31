namespace eWalletWebApp.Exceptions; 

public class ErrorHandler {
    public static void HandleCustomException<T>(T exception) where T : CustomException {
        if (exception is CustomException) {
            // Handle custom exception
        }
    }
}