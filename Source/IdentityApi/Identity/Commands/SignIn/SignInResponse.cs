namespace IdentityApi.Identity.Commands;

public class SignInResponse
{
    public bool IsSuccess { get; init; } 
    public string Message { get; init; }
    public string Token { get; init; }
}