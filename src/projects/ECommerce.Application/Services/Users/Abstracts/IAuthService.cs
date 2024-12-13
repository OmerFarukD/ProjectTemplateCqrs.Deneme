using Core.Security.Dtos;
using Core.Security.JWT;

namespace ECommerce.Application.Services.Users.Abstracts;

public interface IAuthService
{
    Task<AccessToken> LoginAsync(UserForLoginDto dto,CancellationToken cancellationToken);
    Task<AccessToken> RegisterAsync(UserForRegisterDto dto,CancellationToken cancellationToken);
}