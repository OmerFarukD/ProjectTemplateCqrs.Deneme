using System.Linq.Expressions;
using Core.Persistence.Extensions;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.Repositories;
using ECommerce.Application.Services.Users.Abstracts;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.Users.Concretes;

public sealed class UserService(IAppUserRepository _userRepository, UserBusinessRules _userBusinessRules) : IUserService
{
    public async Task<AppUser?> GetAsync(Expression<Func<AppUser, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    
    public async Task<Paginate<AppUser>> GetPaginateAsync(Expression<Func<AppUser, bool>>? predicate = null, Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null, bool include = false, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Paginate<AppUser> userList = await _userRepository.GetPaginateAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<List<AppUser>> GetListAsync(Expression<Func<AppUser, bool>>? predicate = null, Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null, bool include = false, bool withDeleted = false,
        bool enableTracking = false, CancellationToken cancellationToken = default)
    {
        List<AppUser> userList = await _userRepository.GetListAsync(
            predicate,orderBy,include,withDeleted,enableTracking,cancellationToken
        );
        return userList;
    }

    public async Task<AppUser?> AddAsync(AppUser user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);
        
        AppUser? addedUser = await _userRepository.AddAsync(user);
        
        return addedUser;
    }

    public async Task<AppUser> UpdateAsync(AppUser user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user.Id, user.Email);

        AppUser updatedUser = await _userRepository.UpdateAsync(user);

        return updatedUser;
    }
    
    public async Task<AppUser> DeleteAsync(AppUser user, bool permanent = false)
    {
        AppUser deletedUser = await _userRepository.DeleteAsync(user,permanent);

        return deletedUser;
    }
}