using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace UiTests.Web
{
    public sealed class Browser : IDisposable
    {
        public Browser(WebDriverConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            Driver = WebDriverRunner.Run(config.BrowserType, config.IsRemote, config.RemoteUrl);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(0.5));
        }

        ~Browser()
        {
            Dispose(false);
        }

        public IWebDriver Driver { get; private set; }
        public IWait<IWebDriver> Wait { get; private set; }

        public void Open(Uri uri)
        {
            try
            {
                Driver.Navigate().GoToUrl(uri);
                Driver.SwitchTo().ActiveElement();
                Wait.Until(_ => ExecuteScript<bool>("return document.readyState == 'complete';"));
            }
            catch (Exception ex)
            {
                Trace.TraceError("ERROR: {0}; {1}", ex.TargetSite, ex.Message);
                throw;
            }
        }

        public TPage GetPage<TPage>() where TPage : PageObject, new()
        {
            var page = new TPage();
            page.Initialize(this);
            return page;
        }

        public TResult ExecuteScript<TResult>(string javascript, params object[] args)
        {
            return Driver.ExecuteJavaScript<TResult>(javascript, args);
        }

        public Screenshot TakeScreenshot()
        {
            return Driver.TakeScreenshot();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Driver?.Dispose();
            Driver = null;
        }
    }
}
