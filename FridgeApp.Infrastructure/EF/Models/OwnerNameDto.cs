using System.Linq;

namespace FridgeApp.Infrastructure.EF.Models
{
    internal class OwnerNameReadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static OwnerNameReadModel Create(string value)
        {
            var splitOwner = value.Split(' ');
            return new OwnerNameReadModel
            {
                FirstName = splitOwner.First(),
                LastName = splitOwner.Last()
            };
        }
        
        public override string ToString()
            => $"{FirstName} {LastName}";
    }
}