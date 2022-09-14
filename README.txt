Solution consists of four parts:
- Console app (A La ADFS) - simulates ADFS which sends the `username@domain` to the plugin (dll). I couldn't target real ADFS 2019 as it is a part of Windows Server and I didn't have one. I decided not to set up a virtual machine with ADFS as I believe that was not the sense of the task.
- Plugin dll (ADFS Plugin) - that's the actual place where logic is implemented. It does a few things:
	1. Reads user from the stub db (user_accounts.json) and checks if its active.
	2. Authenticates app in Azure AD. I created new tenant and new AD in Azure Portal for this task. This dll is registered as daemon 		app there. So that way I'm using MSAL which was the closest solution that I found to relate to this task.
	3. Calls the protected API (RandomTruthApi) which returns true or false randomly. This API uses Microsoft.Identity.Web for 		authentication and also registered in my Azure AD.
- Installer project. It will install the console app on your machine and put .NET Core publish output folders in the selected folder.

--------------------------------------

Notes
1. MSI installer is located in \Installer Project\Installer Project-SetupFiles.
2. Didn't manage to write unit/integration tests - lack of time.