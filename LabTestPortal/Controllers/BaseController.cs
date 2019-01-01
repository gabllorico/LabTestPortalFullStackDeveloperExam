using System.Web.Mvc;

namespace LabTestPortal.Controllers
{
    public class BaseController : Controller
    {
        protected void Flash(string message)
        {
            TempData["FlashMessage"] = message;
        }

        protected void Alert(string message)
        {
            TempData["AlertMessage"] = message;
        }
    }
}