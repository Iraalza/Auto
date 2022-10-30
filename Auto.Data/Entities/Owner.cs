using Newtonsoft.Json;

namespace Auto.Data.Entities
{
    public partial class Owner
    {
        public Owner()
        {
        }
        public Owner(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Vehicle? Vehicle { get; set; }
     
    }
}
