using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityApi.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi.Identity.Commands;

public class SignInCommand : IRequest<SignInResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class SignInCommandHandler(
    IdentityDbContext context,
    SignInManager<Models.User> signInManager,
    UserManager<Models.User> userManager,
    IOptions<Token> tokenOptions)
    : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly IdentityDbContext _context = context;
    private readonly Token _token = tokenOptions.Value;
    private const string UserNotAuthenticatedMessage = "Email or password was incorrect";

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new SignInResponse()
            {
                IsSuccess = false,
                Message = UserNotAuthenticatedMessage
            };
        }

        var signInResponse = await signInManager.PasswordSignInAsync(user, request.Password, true, false);

        if (!signInResponse.Succeeded)
        {
            return new SignInResponse()
            {
                IsSuccess = false,
                Message = UserNotAuthenticatedMessage
            };
        }

        return new SignInResponse()
        {
            IsSuccess = true,
            Token = await GenerateJwtToken(user),
        };
    }
    
    private async Task<string> GenerateJwtToken(Models.User user)
    {
        var roles = (await userManager.GetRolesAsync(user));
        string role = roles.Count != 0 ? roles[0] : null;
        byte[] secret = Encoding.UTF8.GetBytes(_token.Secret);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Issuer = _token.Issuer,
            Audience = _token.Audience,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}