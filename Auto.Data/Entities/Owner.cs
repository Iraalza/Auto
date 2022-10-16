using Newtonsoft.Json;

namespace Auto.Data.Entities
{
    public partial class Owner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleRegistration { get; set; }

        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }
    }
}
