namespace eWalletWebApp.Exceptions; 

public class NotFoundException : CustomException {
    public override string Code { get; } = "404";

    public NotFoundException(string message="Not Found") : base(message) {
        
    }
}