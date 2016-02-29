using System.Configuration;

namespace UiTests.Web
{
    public sealed class WebDriverConfig
    {
        public string BrowserType { get; set; }
        public bool IsRemote { get; set; }
        public string RemoteUrl { get; set; }

        public static WebDriverConfig LoadFromConfig()
        {
            return new WebDriverConfig
            {
                BrowserType = ConfigurationManager.AppSettings["WebDriver:BrowserType"],
                IsRemote = ConfigurationManager.AppSettings["WebDriver:IsRemote"] == "true",
                RemoteUrl = ConfigurationManager.AppSettings["WebDriver:RemoteUrl"]
            };
        }
    }
}