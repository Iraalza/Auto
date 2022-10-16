using System;
using System.Collections.Generic;
using System.Linq;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Queries
{
    public class OwnerQuery : ObjectGraphType
    {
        private readonly IAutoDatabase db;

        public OwnerQuery(IAutoDatabase db)
        {
            this.db = db;

            Field<ListGraphType<OwnerGraphType>>("Owners", "Запрос возвращающий всех владельцев", resolve: GetAllOwners);

        }

        private IEnumerable<Owner> GetAllOwners(IResolveFieldContext<object> context) => db.ListOwners();
    }
}
