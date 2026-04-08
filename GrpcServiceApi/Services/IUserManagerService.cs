using GrpcServiceApi.Entities;

namespace GrpcServiceApi.Services;

public interface IUserManagerService
{
    public User[] GetAll(Guid userId);
    public User GetById(Guid userId);
    public bool Update(Guid userId, User newUser);
    public bool DeleteById(Guid userId);
}