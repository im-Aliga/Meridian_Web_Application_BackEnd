

using Meridian_Web.Areas.Admin.ViewModels.FeedBack;
using Meridian_Web.Areas.Admin.ViewModels.TeamMember;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/teammember")]
    public class TeamMembersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public TeamMembersController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-teammember-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.TeamMembers
                .Select(u => new ListViewModel(
                  u.Id, u.FullName, u.Position, u.CreatedAt, u.UpdatedAt, _fileService.GetFileUrl(u.BgImageNameInFileSystem, UploadDirectory.TeamMember)))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-teammember-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }



        [HttpPost("add", Name = "admin-teammember-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.TeamMember);

            await AddTeamMember(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-teammember-list");


            async Task AddTeamMember(string imageName, string imageNameInSystem)
            {
                var teamMember = new TeamMember
                {
                    FullName = model.FullName,
                    Position=model.Position,
                    BgImageName = imageName,
                    BgImageNameInFileSystem = imageNameInSystem,
                };

                await _dataContext.TeamMembers.AddAsync(teamMember);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-teammember-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var teamMember = await _dataContext.TeamMembers.FirstOrDefaultAsync(b => b.Id == id);
            if (teamMember is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = teamMember.Id,
                FullName = teamMember.FullName,
                Position = teamMember.Position,
                ImageUrl = _fileService.GetFileUrl(teamMember.BgImageNameInFileSystem, UploadDirectory.TeamMember)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-teammember-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var teamMember = await _dataContext.TeamMembers.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (teamMember is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(teamMember.BgImageNameInFileSystem, UploadDirectory.TeamMember);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.TeamMember);
                await UpdateTeamMemberAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateTeamMemberAsync(teamMember.BgImageName, teamMember.BgImageNameInFileSystem);
            }


            return RedirectToRoute("admin-teammember-list");


            async Task UpdateTeamMemberAsync(string imageName, string imageNameInFileSystem)
            {
                teamMember.FullName = model.FullName;
                teamMember.Position=model.Position;
                teamMember.BgImageName = imageName;
                teamMember.BgImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-teammember-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var teamMember = await _dataContext.TeamMembers.FirstOrDefaultAsync(b => b.Id == id);
            if (teamMember is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(teamMember.BgImageNameInFileSystem, UploadDirectory.TeamMember);

            _dataContext.TeamMembers.Remove(teamMember);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-teammember-list");
        }
        #endregion


    }
}
