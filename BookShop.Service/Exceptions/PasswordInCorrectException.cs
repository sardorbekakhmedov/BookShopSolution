namespace BookShop.Service.Exceptions;

public class PasswordInCorrectException : Exception
{
    public PasswordInCorrectException(string message) : base(message)
    { }
}