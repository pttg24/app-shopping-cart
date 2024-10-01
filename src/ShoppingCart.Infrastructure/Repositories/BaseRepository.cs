using AutoMapper;

namespace ShoppingCart.Infrastructure.Repositories;

public class BaseRepository
{
    public ShoppingCartContext DbContext { get; }
    public IMapper Mapper { get; }

    public BaseRepository(ShoppingCartContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public async Task<bool> SaveChanges()
    {
        return await DbContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return DbContext.ChangeTracker.HasChanges();
    }

}
