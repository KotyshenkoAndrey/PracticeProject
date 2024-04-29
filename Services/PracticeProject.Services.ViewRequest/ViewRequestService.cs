using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Context;
using PracticeProject.Context.Entities;
using PracticeProject.Services.ViewingRequests.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Services.AppHubs;

namespace PracticeProject.Services.ViewingRequests;



public class ViewRequestService : IViewRequest
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IHubContext<AppHub> hubContext;
    private readonly IAction action;

    public ViewRequestService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper
        ,IHubContext<AppHub> hubContext
        ,IAction action
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.hubContext = hubContext;
        this.action = action;
    }
   public async Task<IActionResult> CreateViewingRequest(CreateViewingRequestViewModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        

        var request = mapper.Map<ViewingRequest>(model);
        var ownerCar = context.Cars
                                .Where(c => c.Id == request.CarId)
                                .Select(c => c.Seller.Uid)
                                .FirstOrDefault();
        if (model.SenderId == ownerCar) { return new OkObjectResult("You cannot create a request to view your cars"); }

        var chckDuplicate = await context.ViewingRequests
                                         .FirstOrDefaultAsync(vr => vr.CarId == request.CarId &&
                                                              vr.SenderId == request.SenderId &&
                                                              vr.StateConfirmed != StatusConfirm.Rejected);
        if (chckDuplicate != null) { return new OkObjectResult("There is already a viewing request for this car"); }
        await context.ViewingRequests.AddAsync(request);        
        
        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData();
            var ownerEmail = context.Cars
                .Where(c => c.Id == request.CarId)
                .Select(c => c.Seller.Email)
                .FirstOrDefault();
            var senderFullName = context.Sellers.Where(c => c.Id == request.SenderId).Select(c => c.FullName).FirstOrDefault();
            var carModel = context.Cars.Where(c => c.Id == request.CarId).Select(c => c.Model).FirstOrDefault();
            var emailList = new List<string> { ownerEmail };
            await action.SendMail(new EmailSendModel()
            {
                Receiver = emailList,
                Subject = "A new car sale announcement has been published",
                Body = $"A new request has been received to view the car {carModel} from the user {senderFullName}."
            });
        }
        return new OkObjectResult("Request to view the car has been successful"); ;
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
        viewingRequests.LastModifedDate = DateTime.UtcNow;

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData();
            var ownerEmail = context.Sellers
                .Where(c => c.Id == viewingRequests.SenderId)
                .Select(c => c.Email)
                .FirstOrDefault();
            var carModel = context.Cars
                .Where(c => c.Id == viewingRequests.CarId)
                .Select(c => c.Model)
                .FirstOrDefault();
            var emailList = new List<string> { ownerEmail };
            await action.SendMail(new EmailSendModel()
            {
                Receiver = emailList,
                Subject = state == StatusConfirm.Approve ? $"Request to view the car {carModel} confirmed" :
                                                           $"Request to view the car {carModel} rejected",
                Body =state == StatusConfirm.Approve ?$"The seller has confirmed your request to view the car." +
                $"                                      \r\nThe seller's contact details are available for viewing." :

                                                        "The seller declined your request to view the car." +
                                                        "\r\nYou can request the seller's details again."
            });
        }
    }
    public async Task<int> getCountNewRequest(Guid sellerUid)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers.FirstOrDefaultAsync(c => c.Uid == sellerUid);
        var cars = await context.Cars
                                .Include(s => s.Seller)
                                .Include(s => s.ViewingRequestsCar)
                                .Where(s => s.Seller.Uid == sellerUid)
                                .ToListAsync();
        var carsIds = cars.Select(c => c.Id).ToList();
        var viewingRequestsCount = await context.ViewingRequests
            .Include(vr => vr.Car)
            .Where(vr => carsIds.Contains(vr.CarId) && vr.StateConfirmed == StatusConfirm.Wait)
            .CountAsync();
        return viewingRequestsCount;
    }

    public async Task SendCommandForUpdateData()
    {
        await hubContext.Clients.All.SendAsync("ReceiveIncomeRequestUpdate", "update");
    }
}

