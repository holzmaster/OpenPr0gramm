using System.Collections;
using System.Collections.Generic;

namespace OpenPr0gramm.Generic.Response
{
    public interface IMessageResponse<T>
    {
        IReadOnlyList<T> Messages { get; }
    }
}
