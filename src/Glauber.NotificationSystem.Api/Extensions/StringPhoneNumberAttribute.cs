using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Glauber.NotificationSystem.Api.Extensions;

public partial class StringPhoneNumberAttribute : ValidationAttribute
{
    public StringPhoneNumberAttribute()
    {
        
    }
    public override bool IsValid(object? value)
    {
        string? strValue = value as string;
        if (!string.IsNullOrWhiteSpace(strValue))
        {
            var match = PhoneNumberRegex().Match(strValue);
            return match.Success;
        }
        return true;
    }
    
    [GeneratedRegex("([(][0-9]{2}[)])?[0-9]{4,5}-?[0-9]{4}")]
    private static partial Regex PhoneNumberRegex();
}
