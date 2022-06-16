using System.Text.RegularExpressions;

namespace ZamtelWebAPI.Validators
{
    public static class PhoneNumber
    {
        // Regular expression used to validate a phone number.
        public const string motif = @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }
    }
}
