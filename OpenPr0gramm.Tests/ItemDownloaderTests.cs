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
            var res = await cl.Item.GetItemsOlder(ItemFlags.SFW, ItemStatus.Promoted, false, null, null, null, false, 1196866).ConfigureAwait(false);
            Assert.NotNull(res);
            Assert.That(res.Items, Is.Not.Empty);
            Assert.AreEqual(120, res.Items.Count);
            CollectionAssert.AllItemsAreNotNull(res.Items);
            CollectionAssert.AllItemsAreUnique(res.Items);

            _items = res.Items;

            Assert.That(_items[0].ImageUrl, Does.EndWith(".png"));
            Assert.That(_items[1].ImageUrl, Does.EndWith(".webm"));
            Assert.That(!string.IsNullOrWhiteSpace(_items[10].ImageUrl));
        }

        [Test]
        public async Task ThumbnailTest()
        {
            const string expected = "http://thumb.pr0gramm.com/2016/02/25/c39ca11598127046.jpg";

            var item = _items[0];

            var expectedFile = await DownloadExpectedFile(expected);
            using (var dl = new ItemDownloader(DownloadKind.Thumbnail, true))
            {
                var actualStream = await dl.DownloadItemAsync(item);
                var actualFile = await StreamToFile(actualStream);

                CompareFiles(expectedFile, actualFile);
            }
        }

        [Test]
        public async Task ImageTest()
        {
            const string expected = "http://img.pr0gramm.com/2016/02/25/c39ca11598127046.png";

            var item = _items[0];

            var expectedFile = await DownloadExpectedFile(expected);
            using (var dl = new ItemDownloader(DownloadKind.NormalImage, true))
            {
                var actualStream = await dl.DownloadItemAsync(item);
                var actualFile = await StreamToFile(actualStream);

                CompareFiles(expectedFile, actualFile);
            }
        }

        [Test]
        public async Task FullSizeTest()
        {
            const string expected = "http://full.pr0gramm.com/2016/02/24/1db2dd03de191f02.jpg";

            var item = _items[10];

            var expectedFile = await DownloadExpectedFile(expected);
            using (var dl = new ItemDownloader(DownloadKind.LargestAvailable, true))
            {
                var actualStream = await dl.DownloadItemAsync(item);
                var actualFile = await StreamToFile(actualStream);

                CompareFiles(expectedFile, actualFile);
            }
        }


        [Test]
        public async Task WebmTest()
        {
            const string expected = "http://img.pr0gramm.com/2016/02/25/e140680dcdb3daa4.webm";

            var item = _items[1];
            Assert.NotNull(item);

            var expectedFile = await DownloadExpectedFile(expected);
            using (var dl = new ItemDownloader(DownloadKind.NormalImage, true))
            {
                var actualStream = await dl.DownloadItemAsync(item);
                var actualFile = await StreamToFile(actualStream);

                CompareFiles(expectedFile, actualFile);
            }
        }
        [Test]
        public async Task MpegTest()
        {
            const string expected = "http://img.pr0gramm.com/2016/02/25/e140680dcdb3daa4.mpg";

            var item = _items[1];
            Assert.NotNull(item);

            var expectedFile = await DownloadExpectedFile(expected);
            using (var dl = new ItemDownloader(DownloadKind.NormalImage, true))
            {
                dl.VideoOptions = VideoOptions.Mpeg;

                var actualStream = await dl.DownloadItemAsync(item);
                var actualFile = await StreamToFile(actualStream);

                CompareFiles(expectedFile, actualFile);
            }
        }

        private static async Task DownloadFile(string expectedFile, string url)
        {
            using (var expectedStream = File.OpenWrite(expectedFile))
            using (var cl = new HttpClient())
            {
                var stream = await cl.GetStreamAsync(url).ConfigureAwait(false);
                await stream.CopyToAsync(expectedStream).ConfigureAwait(false);
            }
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
