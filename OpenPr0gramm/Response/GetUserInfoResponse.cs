using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class GetUserInfoResponse : Pr0grammResponse
    {
        public AccountInfo Account { get; set; }
        [JsonProperty(PropertyName = "invited")]
        public IReadOnlyList<InvitedUser> InvitedUsers { get; set; }
    }
}
