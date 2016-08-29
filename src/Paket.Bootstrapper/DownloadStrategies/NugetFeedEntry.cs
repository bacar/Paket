using System.Linq;
using System.ServiceModel.Syndication;

namespace Paket.Bootstrapper.Tests.DownloadStrategies
{
    class NugetFeedEntry
    {
        private readonly string version;
        private readonly System.Uri contentUrl;

        public string Version { get { return version; } }
        public System.Uri ContentUrl { get { return contentUrl; } }

        public NugetFeedEntry(System.Xml.XmlReader xmlReader)
        {
            // parse the atom feed
            var feed = SyndicationFeed.Load(xmlReader);
            var entry = feed.Items.Single();
            var content = (UrlSyndicationContent)entry.Content;
            var contentUrl = content.Url;
            var elementextensions = entry.ElementExtensions.Where(extension => extension.OuterName == "properties").Single().GetObject<System.Xml.Linq.XElement>();
            var version = elementextensions.Elements().Where(x => x.Name.LocalName == "Version").Single().Value;

            this.version = version;
            this.contentUrl = contentUrl;

        }
    }
}
