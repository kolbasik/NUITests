using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace UiTests.Browsers
{
    public sealed class WebDriverContext : IDisposable
    {
        private static readonly Lazy<WebDriverContext> Shared =
            new Lazy<WebDriverContext>(() => new WebDriverContext(WebDriverConfig.LoadFromConfig()), true);

        public WebDriverContext(WebDriverConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            WebDriver = WebDriverRunner.Run(config.BrowserType, config.IsRemote, config.RemoteUrl);
        }

        ~WebDriverContext()
        {
            Dispose(false);
        }

        public IWebDriver WebDriver { get; private set; }

        public TResult ExecuteScript<TResult>(string javascript)
        {
            var executor = (IJavaScriptExecutor) WebDriver;
            return (TResult)executor.ExecuteScript(javascript);
        }

        public Screenshot TakeScreenshot()
        {
            return WebDriver.TakeScreenshot();
        }

        public TPage GetPage<TPage>() where TPage : PageObject, new()
        {
            var page = new TPage();
            page.Initialize(this);
            return page;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            WebDriver?.Dispose();
            WebDriver = null;
        }
    }
}
