using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class CommentUser : IPr0grammUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserMark Mark { get; set; }

        public override string ToString() => $"{Name} ({Id}; {Mark})";
    }

    [Serializable]
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

        public override string ToString() => $"{base.ToString()} (RegisteredSince: {RegisteredSince}, Score: {Score}, IsAdmin: {IsAdmin}, IsBanned: {IsBanned})";
    }

    [Serializable]
    public class FollowedUser : INamedPr0grammUser
    {
        public string ItemId { get; set; }
        [JsonProperty(PropertyName = "thumb")]
        [JsonConverter(typeof(ThumbnailUrlConverter))]
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

    [Serializable]
    public class InvitedUser : INamedPr0grammUser
    {
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        public string Email { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime RegisteredAt { get; set; }
    }

    [Serializable]
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
