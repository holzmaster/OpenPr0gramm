using JsonNet.PrivateSettersContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using OpenPr0gramm.Inbox;

namespace OpenPr0gramm
{
    public class Pr0grammApiClient : IPr0grammApiClient
    {
        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;
        private static readonly string UserAgent = ClientConstants.GetUserAgent(nameof(Pr0grammApiClient));

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
            JsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore }
        };

        public Pr0grammApiClient()
            : this(null)
        { }
        public Pr0grammApiClient(CookieContainer cookieContainer)
        {
            _clientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer ?? new CookieContainer()
            };

            _client = new HttpClient(_clientHandler) { BaseAddress = new Uri(ClientConstants.ApiBaseUrl) };
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

        public CookieContainer GetCookies() => _clientHandler?.CookieContainer;

        public string GetCurrentNonce()
        {
            var sessionId = GetCurrentSessionId();
            return sessionId?.Substring(0, 16);
        }

        public string GetCurrentSessionId()
        {
            return GetMeCookie()?.Id;
        }

        public Pr0grammMeCookie GetMeCookie()
        {
            var container = _clientHandler?.CookieContainer;
            if (container == null)
                return null;
            var cookies = container.GetCookies(new Uri(ClientConstants.ProtocolPrefix + ClientConstants.HostName + "/"));
            var meCookie = cookies["me"]?.Value;
            if (meCookie == null)
                return null;
            meCookie = WebUtility.UrlDecode(meCookie);
            return JsonConvert.DeserializeObject<Pr0grammMeCookie>(meCookie);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _clientHandler.Dispose();
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

    public class Pr0grammMeCookie
    {
        [JsonProperty("n")]
        public string N { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("a")]
        public bool A { get; set; }
        [JsonProperty("pp")]
        public string pp { get; set; }
        [JsonProperty("paid")]
        public bool Paid { get; set; }
        [JsonProperty("verified")]
        public bool Verified { get; set; }
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
        Pr0grammMeCookie GetMeCookie();
    }
}
