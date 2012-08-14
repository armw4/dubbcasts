using System;

namespace UoW.NHibernate
{
    public interface IEntity {}

    public abstract class Entity : Entity<Guid>
    {
    }

    public abstract class Entity<TId> : IEntity
    {
        public virtual TId Id { get; set; }
    }
}
