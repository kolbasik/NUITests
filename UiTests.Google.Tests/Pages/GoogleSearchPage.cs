using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using UiTests.Web;

namespace UiTests.Google.Tests.Pages
{
    public sealed class GoogleSearchPage : PageObject
    {
        [FindsBy(How = How.Name, Using = @"q")]
        public IWebElement SearchQuery { get; set; }

        [FindsBy(How = How.Name, Using = @"btnG")]
        public IWebElement SubmitButton { get; set; }

        public override void Invoke()
        {
            Browser.Open(new Uri("https://www.google.com", UriKind.Absolute));
        }

        public override bool IsDisplayed()
        {
            return string.Equals("Google", Browser.Driver.Title, StringComparison.Ordinal);
        }

        public override void EnsureElements()
        {
            Trace.Assert(SearchQuery != null, nameof(SearchQuery));
            Trace.Assert(SubmitButton != null, nameof(SubmitButton));
        }

        public void EnterSearchQuery(string query)
        {
            SearchQuery.SendKeys(query);
        }

        public void Search()
        {
            SubmitButton.Click();
        }

        public GoogleSearchResults GetSearchResultsBlock()
        {
            return GetElement<GoogleSearchResults>();
        }

        public sealed class GoogleSearchResults : PageElement
        {
            [FindsBy(How = How.CssSelector, Using = "h3.r>a")]
            public IList<IWebElement> Links { get; set; }

            public override bool IsDisplayed()
            {
                return Browser.Wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h3.r>a"))).Displayed;
            }

            public override void EnsureElements()
            {
                Trace.Assert(Links != null);
            }

            public IEnumerable<Tuple<string, string>> GetResults()
            {
                foreach (var link in Links)
                {
                    yield return Tuple.Create(link.Text, link.GetAttribute("href"));
                }
            }
        }
    }
}