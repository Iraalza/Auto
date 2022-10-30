using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Auto.Website.Models
{
    public class OwnerDto
    {
        public OwnerDto()
        {
        }
        public OwnerDto(string firstName, string lastName, string phoneNumber, string regCodeVehicle = null)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            RegCodeVehicle = regCodeVehicle;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string? RegCodeVehicle { get; set; }
    }
    

}
