namespace Constituency.Desktop.Helpers
{
    class Settings
    {

        // private static readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public static string GetApiUrl()
        {
            return "https://localhost:7235/";
             //return "https://constituencygnd.azurewebsites.net/";

        }
    }
}
