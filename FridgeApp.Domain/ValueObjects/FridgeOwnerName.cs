using System.Linq;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeOwnerName(string FirstName, string LastName)
    {
        public static FridgeOwnerName Create(string value)
        {
            var splitName = value.Split(" ");
            return new FridgeOwnerName(splitName.First(), splitName.Last());
        }

        public override string ToString()
            => $"{FirstName} {LastName}";
    }
}