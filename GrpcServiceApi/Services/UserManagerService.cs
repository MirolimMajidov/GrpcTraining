using System.Collections.Concurrent;
using GrpcServiceApi.Entities;

namespace GrpcServiceApi.Services;

public class UserManagerService : IUserManagerService
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public User[] GetAll()
    {
        return _users.Values.ToArray();
    }

    public User GetById(Guid userId)
    {
        _users.TryGetValue(userId, out var user);
        return user;
    }

    public User Create(User user)
    {
        user.Id = Guid.NewGuid();
        _users[user.Id] = user;
        return user;
    }

    public bool Update(Guid userId, User newUser)
    {
        if (!_users.ContainsKey(userId))
            return false;

        _users[userId] = newUser;
        return true;
    }

    public bool DeleteById(Guid userId)
    {
        return _users.TryRemove(userId, out _);
    }
}