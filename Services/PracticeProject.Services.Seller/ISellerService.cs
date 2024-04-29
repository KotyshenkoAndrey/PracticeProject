

namespace PracticeProject.Services.Sellers;



public interface ISellerService
{
    Task<IEnumerable<SellerViewModel>> GetAll();
    Task<SellerViewModel> GetById(Guid carId);
    Task<SellerViewModel> Create(CreateSellerViewModel model);
    Task Update(Guid id, UpdateSellerViewModel model);
    Task Delete(Guid id);
    Task<SellerProfileModel> GetUserProfile(Guid userUid);
    Task<SellerProfileModel> GetSellerContact(Guid requestId);

}

