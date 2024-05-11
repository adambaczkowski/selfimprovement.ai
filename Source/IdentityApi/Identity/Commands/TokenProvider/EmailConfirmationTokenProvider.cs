using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityApi.Identity.Commands.TokenProvider;

public class EmailConfirmationTokenProvider
    : DataProtectorTokenProvider<Models.User> 
{
    public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<EmailConfirmationTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<Models.User>> logger)
        : base(dataProtectionProvider, options, logger)
    {

    }
}
public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
}