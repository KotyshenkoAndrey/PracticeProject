using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context;
using PracticeProject.Context.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PracticeProject.Services.Sellers;



public class SellerService : ISellerService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateSellerViewModel> createSellerModelValidator;
    private readonly IModelValidator<UpdateSellerViewModel> updateSellerModelValidator;
    private readonly IAction action;

    public SellerService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper
        ,IModelValidator<CreateSellerViewModel> createSellerModelValidator
        , IModelValidator<UpdateSellerViewModel> updateSellerModelValidator
        , IAction action
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createSellerModelValidator = createSellerModelValidator;
        this.updateSellerModelValidator = updateSellerModelValidator;
        this.action = action;
    }
   public async Task<IEnumerable<SellerViewModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var sellers = await context.Sellers
            .ToListAsync();

        var res = mapper.Map<IEnumerable<SellerViewModel>>(sellers);
        return res;
    }
    public async Task<SellerViewModel> GetById(Guid sellerId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers

                        .FirstOrDefaultAsync(x => x.Uid == sellerId);

        var res = mapper.Map<SellerViewModel>(seller);
        return res;
    }
    public async Task<SellerViewModel> Create(CreateSellerViewModel model)
    {
        
        await createSellerModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = mapper.Map<Seller>(model);

        await context.Sellers.AddAsync(seller);

        await context.SaveChangesAsync();

        return mapper.Map<SellerViewModel>(seller);
    }
    public async Task Update(Guid id, UpdateSellerViewModel model)
    {
        
        await updateSellerModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers.Where(x => x.Uid == id).FirstOrDefaultAsync();

        seller = mapper.Map(model, seller);

        context.Sellers.Update(seller);

        await context.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (seller == null)
            throw new ProcessException($"seller (ID = {id}) not found.");

        context.Sellers.Remove(seller);

        await context.SaveChangesAsync();
    }

    public async Task<SellerProfileModel> GetUserProfile(Guid userUid)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var seller = await context.Sellers.Where(x => x.Uid == userUid).FirstOrDefaultAsync();
        var sellerModel = mapper.Map<SellerProfileModel>(seller);
        return sellerModel;
    }
    public async Task<SellerProfileModel> GetSellerContact(Guid requestId)//Guid for compatibility with swagger
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var viewingRequest = await context.ViewingRequests
            .Include(vr => vr.Car) 
            .ThenInclude(c => c.Seller) 
            .FirstOrDefaultAsync(vr => vr.Uid == requestId);
        var seller = viewingRequest.Car.Seller;
        var sellerModel = mapper.Map<SellerProfileModel>(seller);
        return sellerModel;
    }
}

