using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context;
using PracticeProject.Context.Entities;
using PracticeProject.Services.ViewingRequests.Models;
using Microsoft.AspNetCore.SignalR;
using PracticeProject.Services.AppCarHub;
using Castle.Core.Smtp;

namespace PracticeProject.Services.ViewingRequests;



public class ViewRequestService : IViewRequest
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IHubContext<CarHub> hubContext;
    private readonly IAction action;

    public ViewRequestService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper
        ,IHubContext<CarHub> hubContext
        ,IAction action
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.hubContext = hubContext;
        this.action = action;
    }
   public async Task<ViewingRequestViewModel> CreateViewingRequest(CreateViewingRequestViewModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var request = mapper.Map<ViewingRequest>(model);

        await context.ViewingRequests.AddAsync(request);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            //            await SendCommandForUpdateData();
            var ownerEmail = context.Cars
                .Where(c => c.Id == request.Id)
                .Select(c => c.Seller.Email)
                .FirstOrDefault();
            //await action.SendMail(new EmailSendModel()
            //{
            //    Receiver = sellersEmail,
            //    Subject = "A new car sale announcement has been published",
            //    Body = $"A new car has been published on the service. Its characteristics:" +
            //    $"\r\nModel:" + (string.IsNullOrEmpty(car.Model) ? "Not specified" : car.Model) +
            //    $"\r\nYear:" + (string.IsNullOrEmpty(car.Year.ToString()) ? "Not specified" : car.Year.ToString()) +
            //    $"\r\nColor:" + (string.IsNullOrEmpty(car.Color) ? "Not specified" : car.Color) +
            //    $"\r\nFull name of the seller:" + (string.IsNullOrEmpty(sellerFullName) ? "Not specified" : sellerFullName)
            //});

        }
        return mapper.Map<ViewingRequestViewModel>(request);
    }

    public async Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests(Guid sellerUid)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers.FirstOrDefaultAsync(c => c.Uid == sellerUid);
        var cars = await context.Cars
                                .Include(s => s.Seller)
                                .Include(s => s.ViewingRequestsCar)
                                .Where(s => s.Seller.Uid == sellerUid)
                                .ToListAsync();
        var carsIds = cars.Select(c => c.Id).ToList();
        var viewingRequests = await context.ViewingRequests
            .Include(vr => vr.Car)
            .Where(vr => carsIds.Contains(vr.CarId))
            .ToListAsync();

        var res = mapper.Map<IEnumerable<ViewingRequestViewModel>>(viewingRequests);
        return res;

    }


    public async Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests(Guid sellerId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var viewingRequests = await context.ViewingRequests
            .Include(vr => vr.Car)
            .Include(x=> x.Sender)
            .Where(s => s.Sender.Uid == sellerId)
            .ToListAsync();

        var res = mapper.Map<IEnumerable<ViewingRequestViewModel>>(viewingRequests);
        return res;
    }

    public async Task ChangeStatusRequest(Guid idRequest, StatusConfirm state)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var viewingRequests = await context.ViewingRequests.FirstOrDefaultAsync(s => s.Uid == idRequest);
        viewingRequests.StateConfirmed = state;
        await context.SaveChangesAsync();

    }


    public async Task SendCommandForUpdateData()
    {
        await hubContext.Clients.All.SendAsync("ReceiveCarUpdate","555");
    }
}

