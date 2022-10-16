using Newtonsoft.Json;
using System.Collections.Generic;
namespace Auto.Data.Entities {
	public partial class Vehicle {

        public Vehicle()
        {
            Owners = new HashSet<Owner>();
        }
        public string Registration { get; set; }
		public string ModelCode { get; set; }
		public string Color { get; set; }
		public int Year { get; set; }

        [JsonIgnore]
		public virtual Model VehicleModel { get; set; }

        public virtual ICollection<Owner> Owners { get; set; }
    }
}
