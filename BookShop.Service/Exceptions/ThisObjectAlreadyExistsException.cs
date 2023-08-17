namespace BookShop.Service.Exceptions;

public class ThisObjectAlreadyExistsException : Exception
{
    public ThisObjectAlreadyExistsException(string message) : base(message)
    { }
}