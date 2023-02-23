using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {

        [HttpGet("index", Name = "admin-dashboard-index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}
