using System;

namespace FridgeApp.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Base FridgeApp exception.
    /// </summary>
    public abstract class FridgeException : Exception
    {
        protected FridgeException(string message) : base(message)
        {
        }
    }
}