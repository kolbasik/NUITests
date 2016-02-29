using System;
using System.Linq;
using UiTests.Google.Tests.Common;
using UiTests.Google.Tests.Pages;
using Xunit;

namespace UiTests.Google.Tests
{
    public sealed class GoogleSearchPageTests : SharedBrowserTest
    {
        [Fact]
        public void Search_GangnamStyle_ReturnsPsyYouTubeChanelOnTop()
        {
            // arrange
            var expected = Tuple.Create("YouTube", "https://www.youtube.com/watch?v=9bZkp7q19f0");

            // act
            var searchPage = Browser.GetPage<GoogleSearchPage>();
            searchPage.Invoke();
            searchPage.EnterSearchQuery("gangnam style");
            searchPage.Search();

            var results = searchPage.GetSearchResultsBlock();
            Assert.True(results.IsDisplayed());

            var actual = results.GetResults().First();

            // assert
            Assert.Contains(expected.Item1, actual.Item1);
            Assert.Equal(expected.Item2, actual.Item2);
        }
    }
}