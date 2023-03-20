using Meridian_Web.Areas.Admin.ViewModels.Contact;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contact")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public ContactController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        #region List
        [HttpGet("list", Name = "admin-contact-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Contacts
                .Select(c => new ListContactViewModel(c.FirstName, c.Email, c.Subject, c.Message,c.Id))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-contact-delete")]
        public async Task<IActionResult> DeleteAsync(DeleteContactViewModel model)
        {
            var contact = await _dataContext.Contacts.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (contact is null) return NotFound();
            _dataContext.Contacts.Remove(contact);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-contact-list");

        }
        #endregion
    }
}
