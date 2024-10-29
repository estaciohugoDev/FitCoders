using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FitCoders.Domain.Utils
{
    public class CustomEmailValidation : ValidationAttribute
    {
        public static bool IsValid(string email)
        {
            var pattern = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            return pattern.IsMatch(email);
        }
    }
}