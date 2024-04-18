using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

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
}

