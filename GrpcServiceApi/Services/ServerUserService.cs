using Grpc.Core;
using GrpcServiceApi.Entities;

namespace GrpcServiceApi.Services;

public class ServerUserService(IUserManagerService userManagerService) : UserService.UserServiceBase
{
    public override Task<GetAllUsersResponse> GetAll(GetAllUsersRequest request, ServerCallContext context)
    {
        var users = userManagerService.GetAll();
        var response = new GetAllUsersResponse();
        response.Users.AddRange(users.Select(MapToDto));
        return Task.FromResult(response);
    }

    public override Task<GetUserByIdResponse> GetById(GetUserByIdRequest request, ServerCallContext context)
    {
        var user = userManagerService.GetById(Guid.Parse(request.Id));
        var response = new GetUserByIdResponse();
        if (user is not null)
            response.User = MapToDto(user);
        return Task.FromResult(response);
    }

    public override Task<CreateUserResponse> Create(CreateUserRequest request, ServerCallContext context)
    {
        var user = userManagerService.Create(new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age
        });
        return Task.FromResult(new CreateUserResponse { User = MapToDto(user) });
    }

    public override Task<UpdateUserResponse> Update(UpdateUserRequest request, ServerCallContext context)
    {
        var success = userManagerService.Update(Guid.Parse(request.Id), new User
        {
            Id = Guid.Parse(request.Id),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age
        });
        return Task.FromResult(new UpdateUserResponse { Success = success });
    }

    public override Task<DeleteUserResponse> Delete(DeleteUserRequest request, ServerCallContext context)
    {
        var success = userManagerService.DeleteById(Guid.Parse(request.Id));
        return Task.FromResult(new DeleteUserResponse { Success = success });
    }

    private static UserMessage MapToDto(User user) => new()
    {
        Id = user.Id.ToString(),
        FirstName = user.FirstName,
        LastName = user.LastName,
        Age = user.Age
    };
}