using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System.Drawing;

namespace Auto.Website.GraphQL.Mutation
{
    public class OwnerMutation : ObjectGraphType
    {
        private readonly IAutoDatabase _db;

        public OwnerMutation(IAutoDatabase db)
        {
            this._db = db;

            Field<OwnerGraphType>(
                "createOwner",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "firstName" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "lastName" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "phoneNumber" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "vehicleRegistration" }
                    ),
                resolve: context =>
                {
                    var firstName = context.GetArgument<string>("firstName");
                    var lastName = context.GetArgument<string>("lastName");
                    var phoneNumber = context.GetArgument<string>("phoneNumber");
                    var vehicleRegistration = context.GetArgument<string>("vehicleRegistration");

                    var ownerVehicle = db.FindVehicle(vehicleRegistration);

                    var owner = new Owner(firstName, lastName, phoneNumber);

                    owner.Vehicle = _db.FindVehicle(vehicleRegistration);

                    _db.CreateOwner(owner);
                    return owner;
                }
             );
        }
    }
}