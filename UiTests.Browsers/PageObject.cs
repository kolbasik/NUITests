using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UiTests.Browsers
{
    public abstract class PageObject
    {
        protected WebDriverContext WebDriverContext { get; private set; }
        protected IWebDriver WebDriver { get { return WebDriverContext.WebDriver; } }

        public void Initialize(WebDriverContext wdc)
        {
            if (wdc == null) throw new ArgumentNullException(nameof(wdc));
            WebDriverContext = wdc;
            PageFactory.InitElements(wdc.WebDriver, this);
        }

        public abstract void Invoke();

        public abstract bool IsDisplayed();

        public abstract void VerifyElements();

        public void TrackJavascriptErrors()
        {
            var javascript = @"
return function(w, e) {
	var errors = w[e] = [];
	w.onerror = function (error, url, line) {
		var message = 'Error: [' + error + '], url: [' + url + '], line: [' + line + ']';
		errors.push(message);
		return false;
	};
}(window, '__WebDriver_Errors__');";

            WebDriverContext.ExecuteScript<object>(javascript);
        }

        public void VerifyJavascriptErrors()
        {
            var javascript = @"
return function(w, e) {
    var errors = w[e] || [];
    try {
        return errors.join('\n');
    } finally {
        errors.length = 0;
    }
}(window, '__WebDriver_Errors__');";

            var errors = WebDriverContext.ExecuteScript<string>(javascript);
            if (!string.IsNullOrEmpty(errors))
            {
                throw new InvalidOperationException(errors);
            }
        }
    }
}