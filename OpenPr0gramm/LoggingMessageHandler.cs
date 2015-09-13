#if DEBUG

using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    internal class LoggingMessageHandler : DelegatingHandler
    {
        public LoggingMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Trace.WriteLine("Request:");
            Trace.WriteLine(request.ToString());
            if (request.Content != null)
                Trace.WriteLine(await request.Content.ReadAsStringAsync());

            var response = await base.SendAsync(request, cancellationToken);

            Trace.WriteLine("Response:");
            Trace.WriteLine(response.ToString());
            if (response.Content != null)
                Trace.WriteLine(await response.Content.ReadAsStringAsync());
            return response;
        }
    }
}
#endif
