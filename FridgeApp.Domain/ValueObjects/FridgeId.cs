using System;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeId : Id
    {
        public FridgeId(Guid value) : base(value)
        {
        }
    }
}