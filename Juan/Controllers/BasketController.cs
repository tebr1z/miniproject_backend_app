using Microsoft.AspNetCore.Mvc;

namespace Juan.Controllers
{
    public class BasketController:Controller
    {
        public IActionResult Index()
        {
            SetItemToSession();
            var result = GetItemFromSession();

           return Content(result);
        }
        private void SetItemToSession()
        {

        }
        private string GetItemFromSession()
        {
            return "";
        }
    }
}
