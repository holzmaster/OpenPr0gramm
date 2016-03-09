using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class LoadInviteResponse : Pr0grammResponse
    {
        public InvitingUser Inviter { get; private set; }
        public string Email { get; private set; }
    }
}
