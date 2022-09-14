using ADFS_Plugin.Helpers;
using ADFS_Plugin.Models;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ADFS_Plugin
{
    public class UserAccountChecker
    {
        private readonly JsonFileReader jsonFileReader;
        private readonly UserAccountInputValidator userAccountInputValidator;

        public UserAccountChecker()
        {
            jsonFileReader = new JsonFileReader();
            userAccountInputValidator = new UserAccountInputValidator();
        }

        public async Task<bool> LookupAndAuthenticate(string userAccountInput)
        {
            if (userAccountInputValidator.IsUserInputValid(userAccountInput))
            {
                var username = userAccountInput.Split("@");
                var userAccount = jsonFileReader.GetUser(username[0], username[1]);

                if (userAccount.IsActive)
                {
                    AuthenticationConfig config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");

                    var appAuthResult = await AuthenticateApp(config);

                    if (appAuthResult != null)
                    {
                        var httpClient = new HttpClient();
                        var apiCaller = new ProtectedApiCallHelper(httpClient);
                        var apiCallResult = await apiCaller.CallWebApiAndProcessResultASync($"{config.TruthApiBaseAddress}/api/truth", appAuthResult.AccessToken);

                        await LogAuthEvent(apiCallResult);
                        return apiCallResult;
                    }
                }
            }

            return false;
        }

        private async Task<AuthenticationResult> AuthenticateApp(AuthenticationConfig config)
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(new Uri(config.Authority))
                    .Build();

            app.AddInMemoryTokenCache();

            string[] scopes = new string[] { config.TruthApiScope };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                // TODO: Replace with Windows Log Api
                Console.WriteLine("Token acquired \n");

                return result;
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected

                // TODO: Replace with Windows Log Api
                Console.WriteLine("Scope provided is not supported");

                throw new Exception(ex.Message, ex);
            }
        }

        private async Task LogAuthEvent(bool result)
        {
            if (result)
            {
                // TODO: Replace with Windows Log Api
                Console.WriteLine("Web Api result: \n");

                // TODO: Replace with Windows Log Api
                Console.WriteLine(result.ToString());
            }
            else
            {
                throw new AuthenticationException("User Authentication Failed");
            }
        }
    }
}
