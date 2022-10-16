using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public sealed class VehicleModelGraphType : ObjectGraphType<Model>
    {
        public VehicleModelGraphType()
        {
            Name = "model";
            Field(c => c.Name).Description("Имя модели автомобиля");
            Field(c => c.Manufacturer, type: typeof(ManufacturerGraphType)).Description("Производитель");
        }
    }
}
