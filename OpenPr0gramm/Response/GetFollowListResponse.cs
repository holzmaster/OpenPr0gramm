using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetFollowListResponse : Pr0grammResponse
    {
        public IReadOnlyList<FollowedUser> List { get; private set; }
    }
}
