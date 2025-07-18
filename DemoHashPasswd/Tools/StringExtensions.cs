// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

namespace DemoHashPasswd.Tools;

public static class StringExtensions
{
    public static byte[] Hash(this string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        return SHA512.HashData(Encoding.Default.GetBytes(value));
    }
}
