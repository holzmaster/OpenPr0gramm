using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class LoadInviteResponse : Pr0grammResponse
    {
        public InvitingUser Inviter { get; set; }
        public string Email { get; set; }
    }
}
