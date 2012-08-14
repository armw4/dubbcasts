using NHibernate;

namespace UoW.NHibernate
{
    class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public void CreateUsers()
        {
            1000.Times(() =>
            {
                var allenIversonKobeBryant = new User
                {
                    FirstName = "Allen Iverson",
                    LastName = "Kobe Bryant",
                };

                _session.Save(allenIversonKobeBryant);
            });
        }
    }
}
