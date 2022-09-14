using System.Text.RegularExpressions;

namespace A_La_ADFS
{
    public class UserAccountInputValidator
    {
        private Regex userInputFormat = new Regex(@"[a-zA-Z]+@[a-zA-Z]+");

        public bool IsUserInputValid(string input)
        {
            return userInputFormat.IsMatch(input);
        }
    }
}
