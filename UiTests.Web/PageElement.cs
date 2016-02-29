using System;
using OpenQA.Selenium.Support.PageObjects;

namespace UiTests.Web
{
    public abstract class PageElement
    {
        protected Browser Browser { get; private set; }

        internal void Initialize(Browser browser)
        {
            if (browser == null) throw new ArgumentNullException(nameof(browser));
            Browser = browser;
            PageFactory.InitElements(Browser.Driver, this);
        }

        public abstract bool IsDisplayed();

        public abstract void EnsureElements();
    }
}