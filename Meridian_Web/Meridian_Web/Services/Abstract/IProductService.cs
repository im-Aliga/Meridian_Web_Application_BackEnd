using Meridian_Web.Areas.Admin.ViewModels.Product;
using Meridian_Web.Database.Models;

namespace Meridian_Web.Services.Abstract
{
    public interface IProductService
    {
        public  void FindPers(AddViewModel model,Product product);

        public   Task<AddViewModel> GetViewForModel(AddViewModel model);
        //public  Task<UpdateViewModel> GetViewForModel(UpdateViewModel model);

    }
}
