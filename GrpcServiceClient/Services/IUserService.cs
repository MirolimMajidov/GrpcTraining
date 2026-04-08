using GrpcServiceClient.Contracts;
using GrpcServiceClient.Entities;

namespace GrpcServiceClient.Services;

public interface IUserService
{
    public User[] GetAll();
    public User GetById(Guid userId);
    public User Create(CreateUser user);
    public bool Update(Guid userId, UpdateUser newUser);
    public bool DeleteById(Guid userId);
}