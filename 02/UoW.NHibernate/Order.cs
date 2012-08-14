using System;

namespace UoW.NHibernate
{
    public class Order : Entity
    {
        public virtual DateTime DateCreated { get; set; }

        public virtual int ItemCount { get; set; }
    }
}
