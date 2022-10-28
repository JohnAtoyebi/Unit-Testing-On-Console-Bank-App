using System.Text.RegularExpressions;
using BankAppCore.Interfaces;


namespace BankAppCore.Helper
{
    public class Validators : IValidators
    {
        public bool CheckName(string name)
        {
            string strRegex = @"^[A-Z]";

            return PerformRegEx(strRegex, name);
        }

        public bool CheckEmail(string email)
        {
            string strRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return PerformRegEx(strRegex, email);

        }

        public bool CheckPassword(string password)
        {
            string strRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$";
            return PerformRegEx(strRegex, password);
        }


        private static bool PerformRegEx(string pattern, string value)
        {
            Regex re = new(pattern);


            if (re.IsMatch(value))
                return (true);
            else
                return (false);
        }

    }
}
