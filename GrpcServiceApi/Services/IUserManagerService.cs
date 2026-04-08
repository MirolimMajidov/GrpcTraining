using GrpcServiceApi.Entities;

namespace GrpcServiceApi.Services;

public interface IUserManagerService
{
    public User[] GetAll();
    public User GetById(Guid userId);
    public User Create(User user);
    public bool Update(Guid userId, User newUser);
    public bool DeleteById(Guid userId);
}