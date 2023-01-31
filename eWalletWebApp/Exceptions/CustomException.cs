namespace eWalletWebApp.Exceptions; 

public abstract class CustomException : Exception {
    public virtual string Code { get; } = "501";
    public CustomException(string message) : base(message) { }
}