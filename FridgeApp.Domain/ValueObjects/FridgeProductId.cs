using System;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeProductId : Id
    {
        public FridgeProductId(Guid value) : base(value)
        {
        }
    }
}