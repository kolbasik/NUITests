using System;

namespace UiTests.Web
{
    public abstract class PageObject : PageElement
    {
        public TElement GetElement<TElement>() where TElement : PageElement, new()
        {
            var element = new TElement();
            element.Initialize(Browser);
            return element;
        }

        public abstract void Invoke();

        public void TrackJavascriptErrors()
        {
            var javascript = @"
var w = this, e = '__WebDriver_Errors__';
var errors = w[e] = [];
w.onerror = function (error, url, line) {
	var message = 'Error: [' + error + '], url: [' + url + '], line: [' + line + ']';
	errors.push(message);
	return false;
};
return '';";

            Browser.ExecuteScript<string>(javascript);
        }

        public void VerifyJavascriptErrors()
        {
            var javascript = @"
var w = this, e = '__WebDriver_Errors__';
var errors = w[e] || [];
try {
    return errors.join('\n');
} finally {
    errors.length = 0;
}";

            var errors = Browser.ExecuteScript<string>(javascript);
            if (!string.IsNullOrEmpty(errors))
            {
                throw new InvalidOperationException(errors);
            }
        }
    }
}