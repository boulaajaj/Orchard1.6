using System.Web.Mvc;

namespace Pluralsight.Movies.Controllers
{
    [Authorize]
    public class GoodByeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return new ViewResult();
        }
        public string GetThis()
        {
            return "got this?";
        }

        public JsonResult Test()
        {
            return new JsonResult() { Data = "test", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}