using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    // TODO test
    public class ItemDownloader : IDisposable
    {
        private static string UserAgent = ClientConstants.GetUserAgent(nameof(ItemDownloader));

        public VideoOptions VideoOptions { get; set; }
        public DownloadKind DownloadKind { get; set; }
        public bool UsingHttps { get; }

        public HttpClient HttpClient { get; }

        #region Ctors
        public ItemDownloader(DownloadKind downloadKind)
            : this(downloadKind, true)
        { }

        public ItemDownloader(DownloadKind downloadKind, bool useHttps)
        {
            DownloadKind = downloadKind;
            UsingHttps = useHttps;
            HttpClient = CreateHttpClient(downloadKind, useHttps);
        }

        #endregion

        public Task<Stream> DownloadItemAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            // save class members to locals since they can change during method execution
            var kind = DownloadKind;
            var videoOptions = VideoOptions;

            if (!Enum.IsDefined(typeof(DownloadKind), kind)) // TODO consider remove since all code paths throw exceptions anyways
                throw new InvalidOperationException();

            // if it's a thumbnail, easy going.
            if (kind == DownloadKind.Thumbnail)
                return HttpClient.GetStreamAsync(item.ThumbnailUrl);

            // if not, consider webm/mpeg and stuff.
            var type = item.GetItemType();
            switch (type)
            {
                case ItemType.Image:
                    // consider requested quality here
                    switch (kind) // cannot be DownloadKind.Thumbnail
                    {
                        case DownloadKind.NormalImage:
                            return HttpClient.GetStreamAsync(item.ImageUrl);
                        case DownloadKind.LargestAvailable:
                            var bestUrl = string.IsNullOrWhiteSpace(item.FullSizeUrl) ? item.ImageUrl : item.FullSizeUrl;
                            return HttpClient.GetStreamAsync(bestUrl);
                        default:
                            throw new InvalidOperationException();
                    }
                case ItemType.Video:
                    // if it's a webm, consider downloading an mpeg instead.
                    // DownloadKind doesn't matter here, since there is only one video quality
                    switch (videoOptions)
                    {
                        case VideoOptions.Webm:
                            return HttpClient.GetStreamAsync(item.ImageUrl); // webm urls are always in the "image" field
                        case VideoOptions.Mpeg:
                            var mpegUrl = item.GetMpegUrl();
                            return HttpClient.GetStreamAsync(mpegUrl);
                        default:
                            throw new InvalidOperationException();
                    }
                default:
                    throw new InvalidOperationException();
            }
        }

        private static IPr0grammItemsService GetServiceFromClient(IPr0grammApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            return client.Items;
        }
        private static HttpClient CreateHttpClient(DownloadKind downloadKind, bool useHttps)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(GetBaseAddressForDownloadKind(downloadKind, useHttps))
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            return client;
        }
        private static string GetBaseAddressForDownloadKind(DownloadKind downloadKind, bool useHttps)
        {
            switch (downloadKind)
            {
                case DownloadKind.Thumbnail:
                    return ClientConstants.GetThumbnailUrlPrefix(useHttps);
                case DownloadKind.NormalImage:
                    return ClientConstants.GetImageUrlPrefix(useHttps);
                case DownloadKind.LargestAvailable:
                    return ClientConstants.GetFullUrlPrefix(useHttps);
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Dispose() => HttpClient.Dispose();
    }

    public enum DownloadKind
    {
        Thumbnail,
        NormalImage,
        LargestAvailable
        // TODO consider Source
    }

    public enum VideoOptions
    {
        Webm,
        Mpeg
    }
}
