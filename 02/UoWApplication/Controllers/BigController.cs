using System.Web.Mvc;
using UoW.NHibernate;

namespace UoWApplication.Controllers
{
    public class BigController : Controller
    {
        private readonly IBigRepository _bigRepository;
       
        public BigController(IBigRepository bigRepository)
        {
            _bigRepository = bigRepository;
        }

        public ActionResult Create()
        {
            _bigRepository.CreateUsersAndOrders();

            return Content("The really huge really big order has been successfully created.");
        }
    }
}
