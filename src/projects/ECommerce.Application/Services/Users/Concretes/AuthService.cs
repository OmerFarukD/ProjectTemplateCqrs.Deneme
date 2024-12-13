using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.Users.Abstracts;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.Users.Concretes;

public sealed class AuthService(
    UserBusinessRules _rules, 
    IUserWithTokenService _tokenService,
    IUserService _userService
    ) : IAuthService
{


    public async Task<AccessToken> LoginAsync(UserForLoginDto dto,CancellationToken cancellationToken)
    {
        AppUser? user = await _userService.GetAsync(
            predicate: u => u.Email == dto.Email,
            cancellationToken: cancellationToken
        );
        
        await _rules.UserShouldBeExistsWhenSelected(user);
        await _rules.UserPasswordShouldBeMatched(user!, dto.Password);
        
        AccessToken createdAccessToken = await _tokenService.CreateAccessToken(user!);

        return createdAccessToken;

    }


    public async Task<AccessToken> RegisterAsync(UserForRegisterDto dto,CancellationToken cancellationToken)
    {
        HashingHelper.CreatePasswordHash(
            dto.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        
      AppUser  newUser =
            new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
      
      AppUser? createdUser = await _userService.AddAsync(newUser);
      AccessToken createdAccessToken = await _tokenService.CreateAccessToken(createdUser);

      return createdAccessToken;
    }

 

   
}