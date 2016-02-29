using System;
using UiTests.Web;

namespace UiTests.Google.Tests.Common
{
    public abstract class SharedBrowserTest
    {
        private static readonly Lazy<Browser> Shared = new Lazy<Browser>(() => new Browser(WebDriverConfig.LoadFromConfig()), true);

        public Browser Browser { get { return Shared.Value; } }

    }
}
