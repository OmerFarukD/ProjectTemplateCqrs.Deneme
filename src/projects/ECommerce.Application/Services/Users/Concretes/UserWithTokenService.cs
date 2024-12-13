using Core.Security.Entities;
using Core.Security.JWT;
using ECommerce.Application.Services.Repositories;
using ECommerce.Application.Services.Users.Abstracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Users.Concretes;

public sealed class UserWithTokenService : IUserWithTokenService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserWithTokenService(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _tokenHelper = tokenHelper;
        _userOperationClaimRepository = userOperationClaimRepository;
        
    }

    public async Task<AccessToken> CreateAccessToken(AppUser? user)
    {
        IList<OperationClaim> operationClaims = await _userOperationClaimRepository
            .Query()
            .AsNoTracking()
            .Where(p => p.UserId == user.Id)
            .Select(p => new OperationClaim { Id = p.OperationClaimId, Name = p.OperationClaim.Name })
            .ToListAsync();

        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }
}