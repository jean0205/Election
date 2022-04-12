using Constituency.Desktop.Helpers;
using Constituency.Desktop.Views;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;

namespace Constituency.Desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            Application.Run(new Frm_Login());
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Crashes.TrackError(e.Exception);
            UtilRecurrent.ErrorMessage(e.Exception.Message);

        }
    }
}