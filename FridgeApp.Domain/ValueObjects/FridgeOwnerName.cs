using System.Linq;

namespace FridgeApp.Domain.ValueObjects
{
    public record OwnerName(string FirstName, string LastName)
    {
        public static OwnerName Create(string value)
        {
            var splitName = value.Split(" ");
            return new OwnerName(splitName.First(), splitName.Last());
        }

        public override string ToString()
            => $"{FirstName} {LastName}";
    }
}