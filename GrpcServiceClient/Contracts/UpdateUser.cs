namespace GrpcServiceClient.Contracts;

public class UpdateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short Age { get; set; }
}