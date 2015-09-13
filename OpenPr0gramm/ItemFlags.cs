using System;

namespace OpenPr0gramm
{
    [Flags]
    public enum ItemFlags
    {
        SFW = 1,
        NSFW = 2,
        NSFL = 4,
        All = SFW | NSFW | NSFL
    }
}
