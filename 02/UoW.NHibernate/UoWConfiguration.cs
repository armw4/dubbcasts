using System;
using FluentNHibernate.Automapping;

namespace UoW.NHibernate
{
    // w/o this class, Fluent NHibernate would also make interfaces and additional types within the UoW.NHibernate assembly that are not actual entities
    // you can think of this as a filter.
    public class UoWConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            var entityMarkerInterface = typeof(IEntity);
            var isEntityAndIsClass = entityMarkerInterface.IsAssignableFrom(type) && type.IsClass;

            return isEntityAndIsClass;
        }
    }
}
