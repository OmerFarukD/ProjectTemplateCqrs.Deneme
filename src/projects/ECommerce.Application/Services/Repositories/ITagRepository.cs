using Core.Persistence.Repositories;
using Core.Security.Entities;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.Repositories;

public interface ITagRepository : IAsyncRepository<Tag, Guid>
{
    
}

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim, int>
{
    
}