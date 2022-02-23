namespace FridgeApp.Shared.Abstractions.Exceptions
{
    public abstract class BaseNotFoundException : FridgeException
    {
        protected BaseNotFoundException(string message) : base(message)
        {
        }
    }
}