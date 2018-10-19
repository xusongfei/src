using System.Collections.Generic;
using System.Drawing;

namespace Lead.Detect.Base.GlobalPrim
{
    public interface IPrimTypeAttributes
    {
        string Name { get; }

        string DisplayName { get; }

        string Description { get; }

        string Group { get; }

        string DisplayGroup { get; }

        byte MajorVersion { get; }

        byte MinorVersion { get; }

        Image Icon { get; }

        object this[string attrName] { get; }

        IEnumerable<KeyValuePair<string, object>> Attributes { get; }
    }
}