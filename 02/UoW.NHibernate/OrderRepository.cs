using System;
using NHibernate;

namespace UoW.NHibernate
{
    public class OrderRepository : IOrderRepository
    {
        private readonly  ISession _session;

        public OrderRepository(ISession session)
        {
            _session = session;
        }

        public void CreateOrders()
        {
            1000.Times(() =>
            {
                var reebokAddidaOrder = new Order
                {
                    DateCreated = DateTime.Now,
                    ItemCount = 324,
                };

                _session.Save(reebokAddidaOrder);
            });
        }
    }
}
