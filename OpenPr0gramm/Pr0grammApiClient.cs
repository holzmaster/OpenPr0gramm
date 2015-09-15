using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Net;
using System.Net.Http;

namespace OpenPr0gramm
{
    public class Pr0grammApiClient : IPr0grammApiClient
    {
        internal const string OpenPr0grammVersion = "0.3.1"; // Also referenced in AssemblyInfo.cs

        private const string ProtocolPrefix = "https://";
        private const string HostName = "pr0gramm.com";
        private const string ApiBaseUrl = ProtocolPrefix + HostName + "/api";
        private const string UserAgent = "OpenPr0gramm/" + OpenPr0grammVersion;

        internal const string ThumbnailUrlPrefix = ProtocolPrefix + "thumb." + HostName;
        internal const string ImageUrlPrefix = ProtocolPrefix + "img." + HostName;
        internal const string FullUrlPrefix = ProtocolPrefix + "full." + HostName;
        internal const string BadgeUrlPrefix = ProtocolPrefix + HostName + "/media/badges";
        internal const string UserUrlPrefix = ProtocolPrefix + HostName + "/user";

        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public IPr0grammUserService User { get; }
        public IPr0grammTagsService Tags { get; }
        public IPr0grammProfileService Profile { get; }
        public IPr0grammItemsService Items { get; }
        public IPr0grammInboxService Inbox { get; }
        public IPr0grammCommentsService Comments { get; }
        public IPr0grammPaypalService Paypal { get; }
        public IPr0grammContactService Contact { get; }
        public IPr0grammBitcoinService Bitcoin { get; }

        private static RefitSettings _refitSettings = new RefitSettings
        {
            UrlParameterFormatter = new EnumsAsIntegersParameterFormatter(),
            JsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore }
        };

        public Pr0grammApiClient()
            : this(null)
        { }
        public Pr0grammApiClient(CookieContainer cookieContainer)
        {
            HttpMessageHandler handler = _handler = new HttpClientHandler();
            if (cookieContainer != null)
                _handler.CookieContainer = cookieContainer;
#if DEBUG
            handler = new LoggingMessageHandler(handler);
#endif
            _client = new HttpClient(handler) { BaseAddress = new Uri(ApiBaseUrl) };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            User = RestService.For<IPr0grammUserService>(_client, _refitSettings); // Done
            Tags = RestService.For<IPr0grammTagsService>(_client, _refitSettings); // Done
            Profile = RestService.For<IPr0grammProfileService>(_client, _refitSettings); // Done
            Items = RestService.For<IPr0grammItemsService>(_client, _refitSettings); // Done
            Inbox = RestService.For<IPr0grammInboxService>(_client, _refitSettings); // Done
            Comments = RestService.For<IPr0grammCommentsService>(_client, _refitSettings); // Done
            Paypal = RestService.For<IPr0grammPaypalService>(_client, _refitSettings); // Done
            Contact = RestService.For<IPr0grammContactService>(_client, _refitSettings); // Done
            Bitcoin = RestService.For<IPr0grammBitcoinService>(_client, _refitSettings); // Done
        }

        public CookieContainer GetCookies() => _handler?.CookieContainer;

        public string GetCurrentNonce()
        {
            var sessionId = GetCurrentSessionId();
            return sessionId?.Substring(0, 16);
        }

        public string GetCurrentSessionId()
        {
            var container = _handler?.CookieContainer;
            if (container == null)
                return null;
            var cookies = container.GetCookies(new Uri(ProtocolPrefix + HostName + "/"));
            var meCookie = cookies["me"]?.Value;
            if (meCookie == null)
                return null;
            meCookie = WebUtility.UrlDecode(meCookie);
            var cookie = JsonConvert.DeserializeObject<Pr0grammMeCookie>(meCookie);
            return cookie?.Id;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _handler.Dispose();
                    _client.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    internal class Pr0grammMeCookie
    {
        public string N { get; set; }
        public string Id { get; set; }
        public bool A { get; set; }
        public long pp { get; set; }
        public bool Paid { get; set; }
    }

    public interface IPr0grammApiClient : IDisposable
    {
        IPr0grammUserService User { get; }
        IPr0grammTagsService Tags { get; }
        IPr0grammProfileService Profile { get; }
        IPr0grammItemsService Items { get; }
        IPr0grammInboxService Inbox { get; }
        IPr0grammCommentsService Comments { get; }
        IPr0grammPaypalService Paypal { get; }
        IPr0grammContactService Contact { get; }
        IPr0grammBitcoinService Bitcoin { get; }

        CookieContainer GetCookies();
        string GetCurrentNonce();
        string GetCurrentSessionId();
    }
}
