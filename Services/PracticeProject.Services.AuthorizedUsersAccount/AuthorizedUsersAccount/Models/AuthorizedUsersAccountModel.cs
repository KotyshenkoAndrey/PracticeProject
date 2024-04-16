namespace PracticeProject.Services.AuthorizedUsersAccount;

using AutoMapper;
using PracticeProject.Context.Entities.Identity;

public class AuthorizedUsersAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class AuthorizedUsersAccountModelProfile : Profile
{
    public AuthorizedUsersAccountModelProfile()
    {
        CreateMap<AuthorizedUsers, AuthorizedUsersAccountModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}
