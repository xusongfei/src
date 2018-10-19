using System.Collections.Generic;
using System.Text;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;

namespace Lead.Detect.FrameworkExtension.loadUtils
{
    public class SectionWriter<T> where T : IElement
    {
        public SectionWriter(string section, Dictionary<int, T> elements)
        {
            Section = section;
            Elements = elements;
        }

        public Dictionary<int, T> Elements;

        public string Section;

        public string Export()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Section.ToUpper()} BEGIN");
            foreach (var ele in Elements)
            {
                sb.AppendLine($"\t{ele.Key} {ele.Value.Export()}");
            }
            sb.AppendLine($"{Section.ToUpper()} END\r\n");
            return sb.ToString();
        }

    }
}