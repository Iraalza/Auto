using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public sealed class OwnerGraphType : ObjectGraphType<Owner>
    {
        public OwnerGraphType()
        {
            Name = "owner";
            Field(c => c.FirstName).Description("Имя владельца");
            Field(c => c.LastName).Description("Фамилия владельца");
            Field(c => c.PhoneNumber).Description("Номер телефона владельца");
            Field(c => c.Vehicle, nullable: false, type: typeof(VehicleGraphType)).Description("Номер автомобиля");
        }
    }
}
