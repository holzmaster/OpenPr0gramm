
namespace OpenPr0gramm
{
    internal static class ClientConstants
    {
        internal const string OpenPr0grammVersion = "0.3.3"; // Also referenced in AssemblyInfo.cs

        internal const string ProtocolPrefix = "https://";
        internal const string InsecureProtocolPrefix = "http://";

        internal const string HostName = "pr0gramm.com";

        internal const string BaseAddress = ProtocolPrefix + HostName;
        internal const string InsecureBaseAddress = InsecureProtocolPrefix + HostName;

        internal const string ApiBaseUrl = BaseAddress + "/api";

        // c# cannot evaluate functions at compile time (like C++ value templates), this is not cool. :(
        public static string GetUserAgent(string component) => $"OpenPr0gramm/{OpenPr0grammVersion} ({component})";

        private static string GetPrefix(bool secure) => secure ? ProtocolPrefix : InsecureProtocolPrefix;

        internal static string GetImageUrlPrefix(bool secure) => GetPrefix(secure) + "img." + HostName;
        internal static string GetThumbnailUrlPrefix(bool secure) => GetPrefix(secure) + "thumb." + HostName;
        internal static string GetFullSizeUrlPrefix(bool secure) => GetPrefix(secure) + "full." + HostName;

        internal const string BadgeUrlPrefix = ProtocolPrefix + HostName + "/media/badges";
        internal const string UserUrlPrefix = ProtocolPrefix + HostName + "/user";
    }
}
