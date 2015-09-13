using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetProfileInfoResponse : Pr0grammResponse
    {
        public User User { get; set; }
        public IReadOnlyList<ProfileComment> Comments { get; set; }
        public int CommentCount { get; set; }
        public IReadOnlyList<ProfileUpload> Uploads { get; set; }
        public int UploadCount { get; set; }
        public bool LikesArePublic { get; set; }
        public IReadOnlyList<LikedItem> Likes { get; set; }
        public int LikeCount { get; set; }
        public int TagCount { get; set; }
        public IReadOnlyList<ProfileBadge> Badges { get; set; }
        [JsonProperty(PropertyName = "followcount")]
        public int FollowCount { get; set; }
        [JsonProperty(PropertyName = "following")]
        public bool IsFollowing { get; set; }

        public override string ToString() => $"Profile: {User}; {Comments.Count} comments (of {CommentCount}); {Uploads.Count} comments (of {UploadCount}); {TagCount} tags";
    }
}
