using GrpcServiceApi;
using GrpcServiceClient.Contracts;
using GrpcServiceClient.Entities;

namespace GrpcServiceClient.Services;

internal class UserService(GrpcServiceApi.UserService.UserServiceClient client) : IUserService
{
    public User[] GetAll()
    {
        var response = client.GetAll(new GetAllUsersRequest());
        return response.Users.Select(MapToEntity).ToArray();
    }

    public User GetById(Guid userId)
    {
        var response = client.GetById(new GetUserByIdRequest { Id = userId.ToString() });
        return MapToEntity(response.User);
    }

    public User Create(CreateUser createUser)
    {
        var response = client.Create(new CreateUserRequest
        {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            Age = createUser.Age
        });
        return MapToEntity(response.User);
    }

    public bool Update(Guid userId, UpdateUser newUser)
    {
        var response = client.Update(new UpdateUserRequest
        {
            Id = userId.ToString(),
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Age = newUser.Age
        });
        return response.Success;
    }

    public bool DeleteById(Guid userId)
    {
        var response = client.Delete(new DeleteUserRequest { Id = userId.ToString() });
        return response.Success;
    }

    private static User MapToEntity(UserMessage msg) => new()
    {
        Id = Guid.Parse(msg.Id),
        FirstName = msg.FirstName,
        LastName = msg.LastName,
        Age = msg.Age
    };
}