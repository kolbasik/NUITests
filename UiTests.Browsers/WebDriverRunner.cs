using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace UiTests.Web
{
    public static class WebDriverRunner
    {
        public static IWebDriver Run(string browserName, bool isRemote, string remoteUrl)
        {
            var driver = isRemote ? ConnectToRemoteWebDriver(browserName, remoteUrl) : StartEmbededWebDriver(browserName);
            var options = driver.Manage();
            var timeouts = options.Timeouts();
            timeouts.SetPageLoadTimeout(TimeSpan.FromSeconds(10));
            timeouts.SetScriptTimeout(TimeSpan.FromSeconds(2.5));
            options.Window.Maximize();
            return driver;
        }

        private static IWebDriver ConnectToRemoteWebDriver(string browserName, string remoteUrl)
        {
            DesiredCapabilities caps;

            switch (browserName)
            {
                case BrowserNames.Firefox:
                    caps = DesiredCapabilities.Firefox();
                    break;
                case BrowserNames.Chrome:
                    caps = DesiredCapabilities.Chrome();
                    break;
                case BrowserNames.InternetExplorer:
                    caps = DesiredCapabilities.InternetExplorer();
                    break;
                case BrowserNames.PhantomJS:
                    caps = DesiredCapabilities.PhantomJS();
                    break;
                case BrowserNames.HtmlUnit:
                    caps = DesiredCapabilities.HtmlUnit();
                    break;
                case BrowserNames.HtmlUnitWithJavaScript:
                    caps = DesiredCapabilities.HtmlUnitWithJavaScript();
                    break;
                case BrowserNames.Opera:
                    caps = DesiredCapabilities.Opera();
                    break;
                case BrowserNames.Safari:
                    caps = DesiredCapabilities.Safari();
                    break;
                case BrowserNames.IPhone:
                    caps = DesiredCapabilities.IPhone();
                    break;
                case BrowserNames.IPad:
                    caps = DesiredCapabilities.IPad();
                    break;
                case BrowserNames.Android:
                    caps = DesiredCapabilities.Android();
                    break;
                default:
                    throw new ArgumentException(
                        string.Format(
                            @"<{0}> was not recognized as supported browser. This parameter is case sensitive",
                            browserName),
                        "WebDriverOptions.BrowserName");
            }
            var newDriver = new RemoteWebDriver(new Uri(remoteUrl), caps);
            return newDriver;
        }

        private static IWebDriver StartEmbededWebDriver(string browserName)
        {
            switch (browserName)
            {
                case BrowserNames.Firefox:
                    return new FirefoxDriver();
                case BrowserNames.Chrome:
                    return new ChromeDriver();
                case BrowserNames.InternetExplorer:
                    return new InternetExplorerDriver();
                case BrowserNames.PhantomJS:
                    return new PhantomJSDriver();
                case BrowserNames.Safari:
                    return new SafariDriver();
                default:
                    throw new ArgumentException(
                        string.Format(
                            @"<{0}> was not recognized as supported browser. This parameter is case sensitive",
                            browserName),
                        "WebDriverOptions.BrowserName");
            }
        }
    }
}