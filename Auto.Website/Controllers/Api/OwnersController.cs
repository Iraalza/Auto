using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.Models;
using Castle.Core.Internal;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Auto.Messages;
using Microsoft.EntityFrameworkCore;


namespace Auto.Website.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IAutoDatabase db;
        private readonly IBus bus;

        public OwnersController(IAutoDatabase db, IBus bus)
        {
            this.db = db;
            this.bus = bus;
        }

        private dynamic Paginate(string url, int index, int count, int total)
        {
            dynamic links = new ExpandoObject();
            links.self = new { href = url };
            links.final = new { href = $"{url}?index={total - (total % count)}&count={count}" };
            links.first = new { href = $"{url}?index=0&count={count}" };
            if (index > 0) links.previous = new { href = $"{url}?index={index - count}&count={count}" };
            if (index + count < total) links.next = new { href = $"{url}?index={index + count}&count={count}" };
            return links;
        }

        // GET: api/owners
        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = 10)
        {
            var items = db.ListOwners().Skip(index).Take(count);
            var total = db.CountOwners();
            var _links = Paginate("/api/owners", index, count, total);
            var _actions = new
            {
                create = new
                {
                    method = "POST",
                    type = "application/json",
                    name = "Create a new owner",
                    href = "/api/owners"
                },
                delete = new
                {
                    method = "DELETE",
                    name = "Delete a qwner",
                    href = "/api/owners/{id}"
                }
            };
            var result = new
            {
                _links,
                _actions,
                index,
                count,
                total,
                items
            };
            return Ok(result);
        }

        // GET api/owners/74953286628
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var owner = db.FindOwner(id);
            if (owner == default) return NotFound();
            var json = owner.ToDynamic();
            json._links = new
            {
                self = new { href = $"/api/owners/{id}" },
                vehicleModel = new { href = $"/api/vehicles/{owner.Vehicle}" }
            };
            json._actions = new
            {
                update = new
                {
                    method = "PUT",
                    href = $"/api/owners/{id}",
                    accept = "application/json"
                },
                delete = new
                {
                    method = "DELETE",
                    href = $"/api/owners/{id}"
                }
            };
            return Ok(json);
        }

        private void PublishNewOwnerMessage(Owner owner)
        {
            var message = new NewOwnerMessage()
            {
                Registration = owner.Vehicle?.Registration,
                Manufacturer = owner.Vehicle?.VehicleModel?.Manufacturer?.Name,
                ModelName = owner.Vehicle?.VehicleModel?.Name,
                ModelCode = owner.Vehicle?.VehicleModel?.Code,
                Color = owner.Vehicle?.Color,
                Year = owner.Vehicle.Year,
                ListedAtUtc = DateTime.UtcNow,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                PhoneNumber = owner.PhoneNumber,
                
            };
            bus.PubSub.Publish(message);
        }

        [HttpPost]
        [Produces("application/hal+json")]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] OwnerDto ownerDto)
        {
            dynamic result;
            try
            {
                Vehicle vehicle = null;
                if (!string.IsNullOrEmpty(ownerDto.RegCodeVehicle))
                {
                    vehicle = db.FindVehicle(ownerDto.RegCodeVehicle);
                }

                var ownerInContext = db.FindOwner(ownerDto.PhoneNumber);
                if (ownerInContext == null)
                {
                    Owner newOwner = CreateOwner(ownerDto, vehicle);
                    result = new
                    {
                        message = "Создан новый владелец",
                        owner = GetResource(newOwner)
                    };
                    PublishNewOwnerMessage(newOwner);
                    return Ok(result);
                }

                result = new { message = "Владелец с такой почтой уже существует", owner = GetResource(ownerInContext) };
            }
            catch (Exception e)
            {
                result = new { message = e.Message };
            }

            return BadRequest(result);
        }

        private Owner CreateOwner(OwnerDto owner, Vehicle vehicle)
        {
            Owner newOwner = new Owner
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                PhoneNumber = owner.PhoneNumber,
            };

            newOwner.Vehicle = vehicle;

            db.CreateOwner(newOwner);
            return newOwner;
        }

        private dynamic GetResource(Owner owner)
        {
            return GetResource(owner, null);
        }

        private dynamic GetResource(Owner owner, string phoneNumber = null)
        {
            if (phoneNumber != null && owner.PhoneNumber != phoneNumber)
            {
                return null;
            }

            var pathOwner = "/api/owners/";
            var pathVehicle = "/api/vehicles/";
            var ownerDynamic = owner.ToDynamic();

            dynamic links = new ExpandoObject();
            links.self = new
            {
                href = $"{pathOwner}{owner.PhoneNumber}"
            };
            if (owner.Vehicle != null)
                links.vehicle = new
                {
                    href = $"{pathVehicle}{owner.Vehicle.Registration}"
                };

            ownerDynamic._links = links;
            ownerDynamic.actions = new
            {
                update = new
                {
                    href = $"/api/owners/update"
                },
                delete = new
                {
                    href = $"/api/owners/delete/{owner.PhoneNumber}"
                }
            };
            return ownerDynamic;
        }
    }
}
