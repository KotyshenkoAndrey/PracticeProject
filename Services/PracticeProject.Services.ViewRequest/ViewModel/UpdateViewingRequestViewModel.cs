using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.ViewingRequests.Models;



public class UpdateViewingRequestViewModel
{

    public StatusConfirm StateConfirmed { get; set; }
    public DateTime LastModifedDate { get; set; }
}

public class UpdateViewingRequestProfile : Profile
{
    public UpdateViewingRequestProfile()
    {
        CreateMap<UpdateViewingRequestViewModel, ViewingRequest>()
            .AfterMap<UpdateViewingRequestActions>();
    }

    public class UpdateViewingRequestActions : IMappingAction<UpdateViewingRequestViewModel, ViewingRequest>
    {
        public void Process(UpdateViewingRequestViewModel source, ViewingRequest destination, ResolutionContext context)
        {
            destination.StateConfirmed = source.StateConfirmed;
            destination.LastModifedDate = DateTime.Now;
        }
    }
}
