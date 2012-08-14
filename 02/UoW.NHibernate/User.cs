namespace UoW.NHibernate
{
    public class User : Entity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }
    }
}
