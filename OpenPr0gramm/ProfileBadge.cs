using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPr0gramm
{
    public class ProfileBadge
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        public ProfileBadge()
        { }
        public ProfileBadge(string image, string description, string link, DateTime createdAt)
            : this()
        {
            Image = image;
            Description = description;
            Link = link;
            CreatedAt = createdAt;
        }

        public override string ToString() => Description;
    }

    public class DynamicProfileBadge : ProfileBadge
    {
        public string Name { get; set; }
        public string Extra { get; set; }
        public DynamicProfileBadge(string image, string description, string link, DateTime createdAt, string name, string extra)
            : base(image, description, link, createdAt)
        {
            Name = name;
            Extra = extra;
        }

        public static DynamicProfileBadge CreateFromCommentCount(string userName, int commentCount, DateTime newestCommentTime)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));
            foreach (var ccn in CommentCountNames.OrderByDescending(kvp => kvp.Key))
            {
                if (commentCount > ccn.Key)
                {
                    var template = BadgeTemplates[DynamicBadgeType.Comments];
                    var description = string.Format(template.Description ?? string.Empty, ccn.Key);
                    var link = $"{ClientConstants.UserUrlPrefix}/{userName}/comments/before/{newestCommentTime.ToUnixTime()}";
                    return new DynamicProfileBadge(template.Image, description, link, newestCommentTime, template.Name, ccn.Value);
                }
            }
            return null;
        }

        public static DynamicProfileBadge CreateFromRegistrationDate(string userName, DateTime registeredSince)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));
            var diffYears = (DateTime.Now - registeredSince).Days / 365;
            if (diffYears <= 0)
                return null;
            var template = BadgeTemplates[DynamicBadgeType.Years];

            var description = string.Format(template.Description ?? string.Empty, diffYears, diffYears != 1 ? "e" : string.Empty);
            var link = $"{ClientConstants.UserUrlPrefix}/{userName}";
            return new DynamicProfileBadge(template.Image, description, link, registeredSince, template.Name, diffYears.ToString());
        }

        private static Dictionary<DynamicBadgeType, BadgeTemplate> BadgeTemplates = new Dictionary<DynamicBadgeType, BadgeTemplate>
        {
            [DynamicBadgeType.Comments] = new BadgeTemplate("comments", "Hat mehr als {0} Kommentare verfasst", null, null, ClientConstants.BadgeUrlPrefix + "/comments.png"),
            [DynamicBadgeType.Years] = new BadgeTemplate("years", "Hat {0} Jahr{1} auf pr0gramm verschwendet", null, null, ClientConstants.BadgeUrlPrefix + "/years.png")
        };
        private static readonly Dictionary<int, string> CommentCountNames = new Dictionary<int, string>
        {
            [1000] = "1k",
            [5000] = "5k",
            [10000] = "10k"
        };
    }

    internal struct BadgeTemplate
    {
        public string Name { get; }
        public string Description { get; }
        public string Link { get; }
        public string Extra { get; }
        public string Image { get; }
        public BadgeTemplate(string name, string description, string link, string extra, string image)
        {
            Name = name;
            Description = description;
            Link = link;
            Extra = extra;
            Image = image;
        }
    }
    internal enum DynamicBadgeType
    {
        Comments = 0,
        Years = 1
    }
}
