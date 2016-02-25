using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public class ItemDownloader
    {
        public IPr0grammItemsService ItemsService { get; }

        public VideoOptions VideoOptions { get; set; }
        public DownloadKind DownloadKind { get; set; }

        #region Ctors

        // TODO Evaluate
        internal ItemDownloader(IPr0grammApiClient client, DownloadKind whatToDownload)
            : this(GetServiceFromClient(client), whatToDownload)
        { }

        public ItemDownloader(IPr0grammItemsService itemsService, DownloadKind downloadKind)
        {
            if (itemsService == null)
                throw new ArgumentNullException(nameof(itemsService));
            ItemsService = itemsService;
            DownloadKind = downloadKind;
        }

        #endregion

        public Task DownloadItemAsync(IPr0grammItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return DownloadItemAsync(item.Id);
        }
        public Task DownloadItemAsync(int item)
        {
            throw new NotImplementedException();
        }

        private static IPr0grammItemsService GetServiceFromClient(IPr0grammApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            return client.Items;
        }
    }

    public enum DownloadKind
    {
        Thumbnail,
        NormalImage,
        LargestAvailable
        // TODO consider
        // , Source
    }

    public enum VideoOptions
    {
        Webm,
        Mpeg
    }
}
