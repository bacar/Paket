using System;
using System.Linq;
using NUnit.Framework;
using System.ServiceModel.Syndication;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Paket.Bootstrapper.Tests.DownloadStrategies
{
    [TestFixture]
    class NugetFeedParserTests
    {
        [Test]
        public void CanExtractVersionFromFeed()
        {
            using (var xmlReader = GetXmlReaderFromFeedResourceStream())
            {
                var entry = new NugetFeedEntry(xmlReader);
                Assert.That(entry.Version, Is.EqualTo("3.16.4"));
            }
        }

        [Test]
        public void CanExtractContentUrlFromFeed()
        {
            using (var xmlReader = GetXmlReaderFromFeedResourceStream())
            {
                var entry = new NugetFeedEntry(xmlReader);

                Uri expectedUri = new Uri("http://www.nuget.org/api/v2/package/Paket/3.16.4");
                Assert.That(entry.ContentUrl, Is.EqualTo(expectedUri));
            }
        }

        private XmlReader GetXmlReaderFromFeedResourceStream()
        {
            var stream = Assembly.GetAssembly(typeof(NugetFeedParserTests))
                .GetManifestResourceStream("Paket.Bootstrapper.Tests.DownloadStrategies.Nuget.3.16.4.feed.xml");
            return XmlReader.Create(stream);
        }
    }
}
