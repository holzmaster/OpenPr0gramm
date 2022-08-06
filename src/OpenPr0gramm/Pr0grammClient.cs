using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OpenPr0gramm.Endpoint.Inbox.FormData;
using OpenPr0gramm.Endpoint.Inbox.Response;

namespace OpenPr0gramm
{
    public class Pr0grammClient : IDisposable
    {
        private readonly IPr0grammApiClient _client;

        public BitcoinController Bitcoin { get; }
        public CommentController Comment { get; }
        public InboxController Inbox { get; }
        public ContactController Contact { get; }
        public ProfileController Profile { get; }
        public PaypalController Paypal { get; }
        public UserController User { get; }
        public TagController Tag { get; }
        public ItemController Item { get; }

        public Pr0grammClient()
            : this(new Pr0grammApiClient())
        { }
        public Pr0grammClient(CookieContainer cookieContainer)
            : this(new Pr0grammApiClient(cookieContainer))
        { }
        public Pr0grammClient(IPr0grammApiClient apiClient)
        {
            if (apiClient == null)
                throw new ArgumentNullException(nameof(apiClient));
            _client = apiClient;
            Bitcoin = new BitcoinController(_client);
            Comment = new CommentController(_client);
            Inbox = new InboxController(_client);
            Contact = new ContactController(_client);
            Profile = new ProfileController(_client);
            Paypal = new PaypalController(_client);
            User = new UserController(_client);
            Tag = new TagController(_client);
            Item = new ItemController(_client);
        }

        public CookieContainer GetCookies() => _client.GetCookies();

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

    public abstract class Pr0grammController
    {
        protected readonly IPr0grammApiClient Client;
        protected Pr0grammController(IPr0grammApiClient client)
        {
            Client = client;
        }
    }
    public class BitcoinController : Pr0grammController
    {
        internal BitcoinController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<GetPaymentAddressResponse> GetPaymentAddress(string emailAddress, string product, bool termsAccepted)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress));
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException(nameof(product));
            Debug.Assert(product == Pr0miumProducts.ThreeMonths || product == Pr0miumProducts.TwelveMonths);
            return Client.Bitcoin.GetPaymentAddress(new PaymentData(Client.GetCurrentNonce(), emailAddress, product, termsAccepted));
        }
    }
    public class CommentController : Pr0grammController
    {
        internal CommentController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<Pr0grammResponse> Delete(IPr0grammComment comment, string reason)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return Delete(comment.Id, reason);
        }
        public Task<Pr0grammResponse> Delete(int commentId, string reason)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(reason));
            return Client.Comments.Delete(new DeleteCommentData(Client.GetCurrentNonce(), commentId, reason));
        }

        public Task<Pr0grammResponse> SoftDelete(IPr0grammComment comment, string reason)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return SoftDelete(comment.Id, reason);
        }
        public Task<Pr0grammResponse> SoftDelete(int commentId, string reason)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(reason));
            return Client.Comments.SoftDelete(new DeleteCommentData(Client.GetCurrentNonce(), commentId, reason));
        }

        public Task<Pr0grammResponse> Edit(IPr0grammComment comment, string newContent)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return Edit(comment.Id, newContent);
        }
        public Task<Pr0grammResponse> Edit(int commentId, string newContent) => Client.Comments.Edit(new EditCommentData(Client.GetCurrentNonce(), commentId, newContent));

        public Task<Pr0grammResponse> Post(IPr0grammItem item, string content)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Post(item.Id, content);
        }
        public Task<Pr0grammResponse> Post(int itemId, string content) => Post(itemId, content, PostCommentData.NoParentId);
        public Task<Pr0grammResponse> Post(IPr0grammItem item, string content, int parentId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Post(item.Id, content, parentId);
        }
        public Task<Pr0grammResponse> Post(int itemId, string content, int parentId)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            return Client.Comments.Post(new PostCommentData(Client.GetCurrentNonce(), itemId, content, parentId));
        }

        public Task<Pr0grammResponse> Vote(IPr0grammComment comment, Vote absoluteVote)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return Vote(comment.Id, absoluteVote);
        }
        public Task<Pr0grammResponse> Vote(int commentId, Vote absoluteVote) => Client.Comments.Vote(new VoteData(Client.GetCurrentNonce(), commentId, (int)absoluteVote));
    }
    public class InboxController : Pr0grammController
    {
        internal InboxController(IPr0grammApiClient client)
            : base(client)
        { }

        public Task<GetInboxOverviewResponse> GetOverview() => Client.Inbox.GetOverview();
        public Task<GetInboxCommentResponse> GetComments() => Client.Inbox.GetComments();
        public Task<GetInboxNotificationResponse> GetNotifications() => Client.Inbox.GetNotifications();
        public Task<GetInboxConversationResponse> GetConversations(long? older = null) => Client.Inbox.GetConversations(older);
        public Task<GetInboxPrivateMessageResponse> GetMessages(string with, long? older = null) =>
            Client.Inbox.GetMessages(with, older);

        public Task<Pr0grammResponse> SendMessage(IPr0grammUser recipient, string message)
        {
            if (recipient == null)
                throw new ArgumentNullException(nameof(recipient));
            return SendMessage(recipient.Id, message);
        }
        public Task<Pr0grammResponse> SendMessage(int recipientId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));
            return Client.Inbox.PostMessage(new PrivateMessageData(Client.GetCurrentNonce(), recipientId, message));
        }
    }
    public class ContactController : Pr0grammController
    {
        internal ContactController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<Pr0grammResponse> SendMessage(string emailAddress, string subject, string content)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress));
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            return Client.Contact.Send(new ContactData(Client.GetCurrentNonce(), emailAddress, subject, content));
        }
    }
    public class PaypalController : Pr0grammController
    {
        internal PaypalController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<GetCheckoutUrlResponse> GetCheckoutUrl(string emailAddress, string product, bool termsAccepted)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress));
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException(nameof(product));
            Debug.Assert(product == Pr0miumProducts.ThreeMonths || product == Pr0miumProducts.TwelveMonths);
            return Client.Paypal.GetCheckoutUrl(new PaymentData(Client.GetCurrentNonce(), emailAddress, product, termsAccepted));
        }
    }
    public class ProfileController : Pr0grammController
    {
        internal ProfileController(IPr0grammApiClient client)
            : base(client)
        { }

        public Task<GetCommentsResponse> GetCommentsBefore(INamedPr0grammUser user, ItemFlags flags, DateTime before)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return GetCommentsBefore(user.Name, flags, before);
        }
        public Task<GetCommentsResponse> GetCommentsBefore(string name, ItemFlags flags, DateTime before)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return Client.Profile.GetCommentsBefore(name, flags, unchecked((int)before.ToUnixTime()));
        }

        public Task<GetCommentsResponse> GetCommentsAfter(INamedPr0grammUser user, ItemFlags flags, DateTime after)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return GetCommentsAfter(user.Name, flags, after);
        }
        public Task<GetCommentsResponse> GetCommentsAfter(string name, ItemFlags flags, DateTime after)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return Client.Profile.GetCommentsAfter(name, flags, unchecked((int)after.ToUnixTime()));
        }

        public Task<GetProfileInfoResponse> GetInfo(INamedPr0grammUser user, ItemFlags flags)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return GetInfo(user.Name, flags);
        }
        public async Task<GetProfileInfoResponse> GetInfo(string name, ItemFlags flags)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            var info = await Client.Profile.GetInfo(name, flags);
            AddDynamicBadges(info);
            return info;
        }

        public Task<Pr0grammResponse> Follow(INamedPr0grammUser userToFollow)
        {
            if (userToFollow == null)
                throw new ArgumentNullException(nameof(userToFollow));
            return Follow(userToFollow.Name);
        }
        public Task<Pr0grammResponse> Follow(string userToFollow)
        {
            if (string.IsNullOrWhiteSpace(userToFollow))
                throw new ArgumentNullException(nameof(userToFollow));
            return Client.Profile.Follow(new FollowData(Client.GetCurrentNonce(), userToFollow));
        }

        public Task<Pr0grammResponse> Unfollow(INamedPr0grammUser userToUnfollow)
        {
            if (userToUnfollow == null)
                throw new ArgumentNullException(nameof(userToUnfollow));
            return Unfollow(userToUnfollow.Name);
        }
        public Task<Pr0grammResponse> Unfollow(string userToUnfollow)
        {
            if (string.IsNullOrWhiteSpace(userToUnfollow))
                throw new ArgumentNullException(nameof(userToUnfollow));
            return Client.Profile.Unfollow(new FollowData(Client.GetCurrentNonce(), userToUnfollow));
        }

        private static void AddDynamicBadges(GetProfileInfoResponse info)
        {
            if (info?.Badges == null)
                return;

            var badges = info.Badges;
            var newBadges = new List<ProfileBadge>(badges);

            var firstComment = info.Comments.Select(c => c?.CreatedAt).FirstOrDefault();
            var commentBadge = DynamicProfileBadge.CreateFromCommentCount(info.User.Name, info.CommentCount, firstComment.HasValue ? firstComment.Value : DateTime.Now);
            if (commentBadge != null)
                newBadges.Add(commentBadge);

            var yearsBadge = DynamicProfileBadge.CreateFromRegistrationDate(info.User.Name, info.User.RegisteredSince);
            if (yearsBadge != null)
                newBadges.Add(yearsBadge);

            info.Badges = newBadges;
        }
    }
    public class UserController : Pr0grammController
    {
        internal UserController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<Pr0grammResponse> Ban(INamedPr0grammUser user, string reason, int durationInDays)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Ban(user.Name, reason, durationInDays);
        }
        public Task<Pr0grammResponse> Ban(string user, string reason, int durationInDays)
        {
            if (durationInDays < 0)
                throw new ArgumentException($"{nameof(durationInDays)} must be >= 0.");
            return Client.User.Ban(new BanData(Client.GetCurrentNonce(), user, reason, durationInDays));
        }

        public Task<ChangeUserDataResponse> ChangeEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            return Client.User.ChangeEmail(new ChangeEmailData(Client.GetCurrentNonce(), token));
        }
        public Task<ChangeUserDataResponse> ChangePassword(string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(currentPassword))
                throw new ArgumentNullException(nameof(currentPassword));
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));
            return Client.User.ChangePassword(new ChangePasswordData(Client.GetCurrentNonce(), currentPassword, newPassword));
        }
        public Task<GetFollowListResponse> GetFollowList(ItemFlags flags) => Client.User.GetFollowList(flags);
        public Task<GetUserInfoResponse> GetInfo() => Client.User.GetInfo();
        public Task<ChangeUserDataResponse> Invite(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            return Client.User.Invite(new InviteData(Client.GetCurrentNonce(), email));
        }
        public Task<ChangeUserDataResponse> JoinWithInvite(string email, string name, string password, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (password.Length < 2)
                throw new ArgumentException($"{nameof(name)}.Length must be >= 2.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (password.Length < 6)
                throw new ArgumentException($"{nameof(password)}.Length must be >= 6.");
            return Client.User.JoinWithInvite(new JoinWithTokenData(email, name, password, token));
        }
        public Task<TokenResponse> JoinWithToken(string email, string name, string password, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (password.Length < 2)
                throw new ArgumentException($"{nameof(name)}.Length must be >= 2.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (password.Length < 6)
                throw new ArgumentException($"{nameof(password)}.Length must be >= 6.");
            return Client.User.JoinWithToken(new JoinWithTokenData(email, name, password, token));
        }
        public Task<LoadInviteResponse> LoadInvite(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            return Client.User.LoadInvite(token);
        }
        public Task<TokenInfoResponse> LoadPaymentToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            return Client.User.LoadPaymentToken(new TokenActionData(Client.GetCurrentNonce(), token));
        }

        public Task<LogInResponse> LogIn(string name, string password, string captchaToken, string captchaSolution)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(captchaToken))
                throw new ArgumentNullException(nameof(captchaToken));
            if (string.IsNullOrEmpty(captchaSolution))
                throw new ArgumentNullException(nameof(captchaSolution));
            return Client.User.LogIn(new LogInData(name, password, captchaToken, captchaSolution));
        }
        public Task<Pr0grammResponse> LogOut() => Client.User.LogOut(new LogOutData(Client.GetCurrentNonce(), Client.GetCurrentSessionId()));

        public Task<CaptchaResponse> RequestCaptcha()
        {
            return Client.User.RequestCaptcha();
        }
        public Task<TokenResponse> RedeemToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            return Client.User.RedeemToken(new TokenActionData(Client.GetCurrentNonce(), token));
        }
        public Task<ChangeUserDataResponse> RequestEmailChange(string currentPassword, string newEmailAddress)
        {
            if (string.IsNullOrEmpty(currentPassword))
                throw new ArgumentNullException(nameof(currentPassword));
            if (string.IsNullOrWhiteSpace(newEmailAddress))
                throw new ArgumentNullException(nameof(newEmailAddress));
            return Client.User.RequestEmailChange(new RequestEmailChangeData(Client.GetCurrentNonce(), currentPassword, newEmailAddress));

        }
        public Task<ChangeUserDataResponse> ResetPassword(string token, string name, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));
            if (newPassword.Length < 6)
                throw new ArgumentException($"{nameof(newPassword)}.Length must be >= 6.");
            return Client.User.ResetPassword(new ResetPasswordData(token, name, newPassword));
        }
        public Task<Pr0grammResponse> SendPasswordResetMail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(email);
            return Client.User.SendPasswordResetMail(new SendPasswordResetMailData(email));
        }
        public Task<ChangeUserDataResponse> SetSiteSettings(bool likesArePublic, bool showAds, UserStatus userStatus)
        {
            if (!Enum.IsDefined(typeof(UserStatus), userStatus))
                throw new ArgumentException($"Invalid value for {nameof(userStatus)}");
            var statusStr = userStatus.ToString().ToLowerInvariant();
            return Client.User.SetSiteSettings(new SiteSettingsData(Client.GetCurrentNonce(), likesArePublic, showAds, statusStr));
        }
        public Task<SyncResponse> Sync(int lastId) => Client.User.Sync(lastId);
        public Task<SuccessableResponse> Validate(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(new SuccessableResponse(false)); // TODO: Or throw exception?
            return Client.User.Validate(new TokenActionData(Client.GetCurrentNonce(), token));
        }
    }
    public class TagController : Pr0grammController
    {
        internal TagController(IPr0grammApiClient client)
            : base(client)
        { }
        public Task<Pr0grammResponse> Add(IPr0grammItem item, string tag)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Add(item.Id, tag);
        }
        public Task<Pr0grammResponse> Add(int itemId, string tag) => Add(itemId, new[] { tag });
        public Task<Pr0grammResponse> Add(IPr0grammItem item, params string[] tags)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Add(item.Id, tags);
        }
        public Task<Pr0grammResponse> Add(int itemId, params string[] tags)
        {
            if (itemId < 0)
                throw new ArgumentException($"{nameof(itemId)} must be >= 0.");
            return Client.Tags.Add(new AddTagsData(Client.GetCurrentNonce(), itemId, tags));
        }

        public Task<Pr0grammResponse> Delete(IPr0grammItem item, bool banUsers, params IPr0grammTag[] tags)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Delete(item.Id, banUsers, tags.Select(t => t.Id).ToArray());
        }
        public Task<Pr0grammResponse> Delete(int itemId, bool banUsers, params int[] tagIds) => Delete(itemId, banUsers, 1, tagIds);
        public Task<Pr0grammResponse> Delete(IPr0grammItem item, bool banUsers, int durationInDays, params IPr0grammTag[] tags)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Delete(item.Id, banUsers, durationInDays, tags.Select(t => t.Id).ToArray());
        }
        public Task<Pr0grammResponse> Delete(int itemId, bool banUsers, int durationInDays, params int[] tagIds)
        {
            if (tagIds == null || tagIds.Length == 0)
                throw new ArgumentException($"{tagIds} cannot be empty.");
            return Client.Tags.Delete(new DeleteTagsData(Client.GetCurrentNonce(), itemId, banUsers, durationInDays, tagIds));
        }

        public Task<GetDetailsResponse> GetDetails(IPr0grammItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return GetDetails(item.Id);
        }
        public Task<GetDetailsResponse> GetDetails(int itemId)
        {
            if (itemId < 0)
                throw new ArgumentException($"{nameof(itemId)} must be >= 0.");
            return Client.Tags.GetDetails(itemId);
        }

        public Task<Pr0grammResponse> Vote(IPr0grammTag tag, Vote absoluteVote)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return Vote(tag.Id, absoluteVote);
        }
        public Task<Pr0grammResponse> Vote(int tagId, Vote absoluteVote)
        {
            if (tagId < 0)
                throw new ArgumentException($"{nameof(tagId)} must be >= 0.");
            return Client.Tags.Vote(new VoteData(Client.GetCurrentNonce(), tagId, (int)absoluteVote));
        }
    }
    public class ItemController : Pr0grammController
    {
        internal ItemController(IPr0grammApiClient client)
            : base(client)
        { }

        public Task<Pr0grammResponse> Delete(IPr0grammItem item, string reason, bool notifyUser, bool banUser, int days)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Delete(item.Id, reason, notifyUser, banUser, days);
        }
        public Task<Pr0grammResponse> Delete(int itemId, string reason, bool notifyUser, bool banUser, int days)
        {
            if (itemId < 0)
                throw new ArgumentException($"{nameof(itemId)} must be >= 0.");
            if (days < 0)
                throw new ArgumentException($"{nameof(days)} must be >= 0.");
            // REMARKS: reason == customreason
            return Client.Items.Delete(new DeleteItemData(Client.GetCurrentNonce(), itemId, reason, reason, notifyUser, banUser, days));
        }

        public Task<GetItemsResponse> GetItems(ItemFlags flags, ItemStatus status) => GetItems(flags, status, false, null, null, null, false);
        public Task<GetItemsResponse> GetItemsByTag(ItemFlags flags, ItemStatus status, string tags) => GetItems(flags, status, false, tags, null, null, false);
        public Task<GetItemsResponse> GetItemsByUser(ItemFlags flags, ItemStatus status, INamedPr0grammUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return GetItemsByUser(flags, status, user.Name);
        }
        public Task<GetItemsResponse> GetItemsByUser(ItemFlags flags, ItemStatus status, string user) => GetItems(flags, status, false, null, user, null, false);
        public Task<GetItemsResponse> GetItemsByCollection(ItemFlags flags, ItemStatus status, string user, string collection) => GetItems(flags, status, false, null, user, collection, false);
        public Task<GetItemsResponse> GetItemsBySelf(ItemFlags flags, ItemStatus status) => GetItems(flags, status, false, null, null, null, true);
        internal Task<GetItemsResponse> GetItems(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self)
        {
            return Client.Items.GetItems(
                flags,
                (int)status,
                following ? 1 : 0,
                string.IsNullOrEmpty(tags) ? null : tags,
                string.IsNullOrEmpty(user) ? null : user,
                string.IsNullOrEmpty(collection) ? null : collection,
                self ? 1 : 0);
        }

        public Task<GetItemsResponse> GetItemsNewer(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, IPr0grammItem newerThan)
        {
            if (newerThan == null)
                throw new ArgumentNullException(nameof(newerThan));
            return GetItemsNewer(flags, status, following, tags, user, collection, self, newerThan.Id);
        }
        public Task<GetItemsResponse> GetItemsNewer(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, int newerThan)
        {
            return Client.Items.GetItemsNewer(flags,
                (int)status,
                following ? 1 : 0,
                string.IsNullOrEmpty(tags) ? null : tags,
                string.IsNullOrEmpty(user) ? null : user,
                string.IsNullOrEmpty(collection) ? null : collection,
                self ? 1 : 0,
                newerThan);
        }

        public Task<GetItemsResponse> GetItemsOlder(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, IPr0grammItem olderThan)
        {
            if (olderThan == null)
                throw new ArgumentNullException(nameof(olderThan));
            return GetItemsOlder(flags, status, following, tags, user, collection, self, olderThan.Id);
        }
        public Task<GetItemsResponse> GetItemsOlder(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, int olderThan)
        {
            return Client.Items.GetItemsOlder(flags,
                (int)status,
                following ? 1 : 0,
                string.IsNullOrEmpty(tags) ? null : tags,
                string.IsNullOrEmpty(user) ? null : user,
                string.IsNullOrEmpty(collection) ? null : collection,
                self ? 1 : 0,
                olderThan);
        }

        public Task<GetItemsResponse> GetItemsAround(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, IPr0grammItem aroundItem)
        {
            if (aroundItem == null)
                throw new ArgumentNullException(nameof(aroundItem));
            return GetItemsAround(flags, status, following, tags, user, collection, self, aroundItem.Id);
        }
        public Task<GetItemsResponse> GetItemsAround(ItemFlags flags, ItemStatus status, bool following, string tags, string user, string collection, bool self, int aroundId)
        {
            return Client.Items.GetItemsAround(flags,
                (int)status,
                following ? 1 : 0,
                string.IsNullOrEmpty(tags) ? null : tags,
                string.IsNullOrEmpty(user) ? null : user,
                string.IsNullOrEmpty(collection) ? null : collection,
                self ? 1 : 0,
                aroundId);
        }

        public Task<GetItemsInfoResponse> Vote(IPr0grammItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return GetInfo(item.Id);
        }
        public Task<GetItemsInfoResponse> GetInfo(int itemId)
        {
            if (itemId < 0)
                throw new ArgumentException($"{nameof(itemId)} must be >= 0.");
            return Client.Items.GetInfo(itemId);
        }

        public Task<Pr0grammResponse> RateLimited() => Client.Items.RateLimited();

        public Task<Pr0grammResponse> Vote(IPr0grammItem item, Vote absoluteVote)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return Vote(item.Id, absoluteVote);
        }
        public Task<Pr0grammResponse> Vote(int itemId, Vote absoluteVote)
        {
            if (itemId < 0)
                throw new ArgumentException($"{nameof(itemId)} must be >= 0.");
            return Client.Items.Vote(new VoteData(Client.GetCurrentNonce(), itemId, (int)absoluteVote));
        }
    }

    public enum UserStatus
    {
        Default,
        Paid
    }
}
