using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class SiteSettingsData : PostFormData
    {
        [AliasAs("likesArePublic")]
        public int LikesArePublic { get; }
        [AliasAs("showAds")]
        public int ShowAds { get; }
        [AliasAs("userStatus")]
        public string UserStatus { get; }
        public SiteSettingsData(string nonce, bool likesArePublic, bool showAds, string userStatus)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(userStatus));
            LikesArePublic = likesArePublic ? 1 : 0;
            ShowAds = showAds ? 1 : 0;
            UserStatus = userStatus;
        }
    }
}
