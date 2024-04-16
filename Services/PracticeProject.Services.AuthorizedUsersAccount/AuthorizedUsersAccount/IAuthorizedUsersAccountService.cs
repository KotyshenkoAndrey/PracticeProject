namespace PracticeProject.Services.AuthorizedUsersAccount;

public interface IAuthorizedUsersAccountService
{
    Task<bool> IsEmpty();
    Task<AuthorizedUsersAccountModel> Create(RegisterAuthorizedUsersAccountModel model);
}
