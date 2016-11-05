using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammUserService
    {
        [Post("/user/ban")]
        Task<Pr0grammResponse> Ban([Body(BodySerializationMethod.UrlEncoded)]BanData data);

        [Post("/user/changeemail")]
        Task<ChangeUserDataResponse> ChangeEmail([Body(BodySerializationMethod.UrlEncoded)]ChangeEmailData data);

        [Post("/user/changepassword")]
        Task<ChangeUserDataResponse> ChangePassword([Body(BodySerializationMethod.UrlEncoded)]ChangePasswordData data);

        [Get("/user/followlist")]
        Task<GetFollowListResponse> GetFollowList(ItemFlags flags);

        [Get("/user/info")]
        Task<GetUserInfoResponse> GetInfo();

        [Post("/user/invite")]
        Task<ChangeUserDataResponse> Invite([Body(BodySerializationMethod.UrlEncoded)]InviteData data);

        [Post("/user/joinwithinvite")]
        Task<ChangeUserDataResponse> JoinWithInvite([Body(BodySerializationMethod.UrlEncoded)]JoinWithTokenData data);

        [Post("/user/joinwithtoken")]
        Task<TokenResponse> JoinWithToken([Body(BodySerializationMethod.UrlEncoded)]JoinWithTokenData data);

        [Get("/user/loadinvite")]
        Task<LoadInviteResponse> LoadInvite(string token);

        [Post("/user/loadpaymenttoken")]
        Task<TokenInfoResponse> LoadPaymentToken([Body(BodySerializationMethod.UrlEncoded)]TokenActionData data);

        [Post("/user/login")]
        Task<LogInResponse> LogIn([Body(BodySerializationMethod.UrlEncoded)]LogInData data);

        [Post("/user/logout")]
        Task<Pr0grammResponse> LogOut([Body(BodySerializationMethod.UrlEncoded)]LogOutData data);

        [Post("/user/redeemtoken")]
        Task<TokenResponse> RedeemToken([Body(BodySerializationMethod.UrlEncoded)]TokenActionData data);

        [Post("/user/requestemailchange")]
        Task<ChangeUserDataResponse> RequestEmailChange([Body(BodySerializationMethod.UrlEncoded)]RequestEmailChangeData data);

        [Post("/user/resetpassword")]
        Task<ChangeUserDataResponse> ResetPassword([Body(BodySerializationMethod.UrlEncoded)]ResetPasswordData data);

        [Post("/user/sendpasswordresetmail")]
        Task<Pr0grammResponse> SendPasswordResetMail([Body(BodySerializationMethod.UrlEncoded)]SendPasswordResetMailData data);

        [Post("/user/sitesettings")]
        Task<ChangeUserDataResponse> SetSiteSettings([Body(BodySerializationMethod.UrlEncoded)]SiteSettingsData data);

        [Get("/user/sync")]
        Task<SyncResponse> Sync(int offset);

        [Post("/user/validate")]
        Task<SuccessableResponse> Validate([Body(BodySerializationMethod.UrlEncoded)]TokenActionData data);
    }
}
