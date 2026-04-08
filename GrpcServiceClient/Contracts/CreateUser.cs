namespace GrpcServiceClient.Contracts;

public class CreateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short Age { get; set; }
}