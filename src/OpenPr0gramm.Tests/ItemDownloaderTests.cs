using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenPr0gramm.Tests
{
    [TestFixture]
    public class ItemDownloaderTests
    {
        private IReadOnlyList<Item> _items;

        [OneTimeSetUp]
        public async Task GetItems()
        {
            var cl = new Pr0grammClient();
            var res = await cl.Item.GetItemsOlder(
                ItemFlags.SFW,
                ItemStatus.New,
                false,
                null,
                null,
                null,
                false,
                1196866
            ).ConfigureAwait(false);

            Assert.NotNull(res);
            Assert.That(res.Items, Is.Not.Empty);
            Assert.AreEqual(120, res.Items.Count);
            CollectionAssert.AllItemsAreNotNull(res.Items);
            CollectionAssert.AllItemsAreUnique(res.Items);

            _items = res.Items;

            // foreach (var item in _items)
            //     System.Console.Error.WriteLine(item.ImageUrl);

            Assert.AreEqual(_items[0].Id, 1196864);
            Assert.That(_items[0].ImageUrl, Does.EndWith(".jpg"));

            Assert.AreEqual(_items[1].Id, 1196863);
            Assert.That(_items[1].ImageUrl, Does.EndWith(".mp4"));

            Assert.AreEqual(_items[2].Id, 1196862);
            Assert.That(_items[2].ImageUrl, Does.EndWith(".jpg"));

            Assert.AreEqual(_items[10].Id, 1196850);
            Assert.That(_items[10].ImageUrl, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task ThumbnailTest()
        {
            const string expected = "http://thumb.pr0gramm.com/2016/02/25/4d99cd2f33f62273.jpg";

            var item = _items[0];

            var expectedFile = await DownloadExpectedFile(expected);

            using var dl = new ItemDownloader(DownloadKind.Thumbnail, true);
            var actualStream = await dl.DownloadItemAsync(item);
            var actualFile = await StreamToFile(actualStream);

            CompareFiles(expectedFile, actualFile);
        }

        [Test]
        public async Task ImageTest()
        {
            const string expected = "http://img.pr0gramm.com/2016/02/25/4d99cd2f33f62273.jpg";

            var item = _items[0];

            var expectedFile = await DownloadExpectedFile(expected);

            using var dl = new ItemDownloader(DownloadKind.NormalImage, true);
            var actualStream = await dl.DownloadItemAsync(item);
            var actualFile = await StreamToFile(actualStream);

            CompareFiles(expectedFile, actualFile);
        }

        [Test]
        public async Task FullSizeTest()
        {
            const string expected = "http://full.pr0gramm.com/2016/02/25/4d99cd2f33f62273.jpg";

            var item = _items[0];

            var expectedFile = await DownloadExpectedFile(expected);

            using var dl = new ItemDownloader(DownloadKind.LargestAvailable, true);
            var actualStream = await dl.DownloadItemAsync(item);
            var actualFile = await StreamToFile(actualStream);

            CompareFiles(expectedFile, actualFile);
        }


        [Test]
        public async Task Mp4Test()
        {
            const string expected = "http://img.pr0gramm.com/2016/02/25/956743d57e310de0.mp4";

            var item = _items[2];
            Assert.NotNull(item);

            var expectedFile = await DownloadExpectedFile(expected);

            using var dl = new ItemDownloader(DownloadKind.NormalImage, true);
            var actualStream = await dl.DownloadItemAsync(item);
            var actualFile = await StreamToFile(actualStream);

            CompareFiles(expectedFile, actualFile);
        }

        private static async Task DownloadFile(string expectedFile, string url)
        {
            using var expectedStream = File.OpenWrite(expectedFile);
            using var cl = new HttpClient();
            cl.DefaultRequestHeaders.UserAgent.ParseAdd("OpenPr0gramm/1.0 (ApiTests)");

            var stream = await cl.GetStreamAsync(url).ConfigureAwait(false);
            await stream.CopyToAsync(expectedStream).ConfigureAwait(false);
        }
        private static async Task<string> DownloadExpectedFile(string url)
        {
            var name = Path.GetTempFileName();
            await DownloadFile(name, url).ConfigureAwait(false);
            return name;
        }
        private static async Task<string> StreamToFile(Stream source)
        {
            var res = Path.GetTempFileName();
            using (var str = File.OpenWrite(res))
                await source.CopyToAsync(str).ConfigureAwait(false);
            return res;
        }

        private static void CompareFiles(string expected, string actual)
        {
            var expectedBytes = File.ReadAllBytes(expected);
            var actualBytes = File.ReadAllBytes(actual);
            CollectionAssert.AreEqual(expectedBytes, actualBytes);
        }
    }
}
