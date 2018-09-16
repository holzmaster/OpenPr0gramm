using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class CommentUser : IPr0grammUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserMark Mark { get; set; }

        public override string ToString() => $"{Name} ({Id}; {Mark})";
    }

#if FW
    [Serializable]
#endif
    public class User : CommentUser
    {
        [JsonProperty(PropertyName = "registered")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime RegisteredSince { get; set; }
        public int Score { get; set; }
        [JsonProperty(PropertyName = "admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty(PropertyName = "banned")]
        public bool IsBanned { get; set; }
        [JsonProperty(PropertyName = "bannedUntil")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? BannedUntil { get; set; }

        public override string ToString() => $"{base.ToString()} (RegisteredSince: {RegisteredSince}, Score: {Score}, IsAdmin: {IsAdmin}, IsBanned: {IsBanned})";
    }

#if FW
    [Serializable]
#endif
    public class FollowedUser : INamedPr0grammUser
    {
        public string ItemId { get; set; }
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "thumb")]
        public string ThumbnailUrl { get; set; }
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        [JsonProperty(PropertyName = "lastPost")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastPostAt { get; set; }
        [JsonProperty(PropertyName = "followCreated")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FollowingSince { get; set; }
    }

#if FW
    [Serializable]
#endif
    public class InvitedUser : INamedPr0grammUser
    {
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        public string Email { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime RegisteredAt { get; set; }
    }

#if FW
    [Serializable]
#endif
    public class InvitingUser : INamedPr0grammUser
    {
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        public string Email { get; set; }
    }

    public interface INamedPr0grammUser
    {
        string Name { get; }
    }

    public interface IPr0grammUser : INamedPr0grammUser
    {
        int Id { get; }
    }
}
