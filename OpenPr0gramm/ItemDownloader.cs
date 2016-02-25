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

        #region Ctors

        // TODO Evaluate
        internal ItemDownloader(IPr0grammApiClient client)
            : this(GetServiceFromClient(client))
        { }

        public ItemDownloader(IPr0grammItemsService itemsService)
        {
            if (itemsService == null)
                throw new ArgumentNullException(nameof(itemsService));
            ItemsService = itemsService;
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

    [Flags]
    enum DownloadOptions
    {

    }
}
