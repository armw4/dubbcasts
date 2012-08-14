using System;

namespace NHibernatePerformanceTip.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }
    }
}
