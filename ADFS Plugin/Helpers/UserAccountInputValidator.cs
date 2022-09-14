using System.Text.RegularExpressions;

namespace ADFS_Plugin.Helpers
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
