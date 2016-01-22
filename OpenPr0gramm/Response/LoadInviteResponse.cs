using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class LoadInviteResponse : Pr0grammResponse
    {
        public InvitingUser Inviter { get; set; }
        public string Email { get; set; }
    }
}
