using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class GetProfileInfoResponse : Pr0grammResponse
    {
        public User User { get; private set; }
        public IReadOnlyList<ProfileComment> Comments { get; private set; }
        public int CommentCount { get; private set; }
        public IReadOnlyList<ProfileUpload> Uploads { get; private set; }
        public int UploadCount { get; private set; }
        public bool LikesArePublic { get; private set; }
        public IReadOnlyList<LikedItem> Likes { get; private set; }
        public int LikeCount { get; private set; }
        public int TagCount { get; private set; }
        public IReadOnlyList<ProfileBadge> Badges { get; internal set; }
        [JsonProperty(PropertyName = "followcount")]
        public int FollowCount { get; private set; }
        [JsonProperty(PropertyName = "following")]
        public bool IsFollowing { get; private set; }

        public override string ToString() => $"Profile: {User}; {Comments.Count} comments (of {CommentCount}); {Uploads.Count} comments (of {UploadCount}); {TagCount} tags";
    }
}
