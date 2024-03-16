using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityApi.Data;
using IdentityApi.Models;
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

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly IdentityDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly Token _token;
    private const string userNotAuthenticatedMessage = "Email or password was incorrect";

    public SignInCommandHandler(IdentityDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<Token> tokenOptions)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _token = tokenOptions.Value;
    }

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new SignInResponse()
            {
                IsSuccess = false,
                Message = userNotAuthenticatedMessage
            };
        }

        var signInResponse = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

        if (!signInResponse.Succeeded)
        {
            return new SignInResponse()
            {
                IsSuccess = false,
                Message = userNotAuthenticatedMessage
            };
        }

        return new SignInResponse()
        {
            IsSuccess = true,
            Token = await GenerateJwtToken(user),
        };
    }
    
    private async Task<string> GenerateJwtToken(User user)
    {
        var roles = (await _userManager.GetRolesAsync(user));
        string role = roles.Count != 0 ? roles[0] : null;
        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Issuer = _token.Issuer,
            Audience = _token.Audience,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}