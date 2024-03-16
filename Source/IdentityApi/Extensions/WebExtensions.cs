using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace IdentityApi.Extensions;

public static class WebExtensions
{
    public const string origin = "localhost:3000"; 
    public static string WebEncodeCode(string code) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    public static string WebDecodeCode(string code) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
}