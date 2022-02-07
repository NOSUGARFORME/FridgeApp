using System;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeModelId : Id
    {
        public FridgeModelId(Guid value) : base(value)
        {
        }
    }
}