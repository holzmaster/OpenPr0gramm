using System;

namespace OpenPr0gramm
{
    [Flags]
    public enum ItemFlags
    {
        SFW = 1,
        NSFW = 2,
        NSFL = 4,
        NSFP = 8,
        AllPublic = SFW | NSFW | NSFL,
        All = AllPublic | NSFP
    }
}
