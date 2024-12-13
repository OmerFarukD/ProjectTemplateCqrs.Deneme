using Core.Security.JWT;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.Users.Abstracts;

public interface IUserWithTokenService
{
    public Task<AccessToken> CreateAccessToken(AppUser? user);
}