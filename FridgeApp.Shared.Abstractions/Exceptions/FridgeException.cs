using System;

namespace FridgeApp.Shared.Abstractions.Exceptions
{
    public abstract class FridgeException : Exception
    {
        protected FridgeException(string message) : base(message)
        {
        }
    }
}