using UiTests.Google.Tests.Common;
using UiTests.Google.Tests.Pages;
using UiTests.Web;
using Xunit;

namespace UiTests.Google.Tests
{
    public sealed class SmokePagesTests : SharedBrowserTest
    {
        [Fact]
        public void GoogleSearchPage_VerifyExpectedElements()
        {
            // arrange
            var page = Browser.GetPage<GoogleSearchPage>();

            // act & assert
            SmokePageTest(page);
        }

        private static void SmokePageTest(PageObject page)
        {
            page.Invoke();
            Assert.True(page.IsDisplayed());
            page.TrackJavascriptErrors();
            page.EnsureElements();
            page.VerifyJavascriptErrors();
        }
    }
}
