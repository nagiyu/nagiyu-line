using System.Collections.Generic;

namespace Line.Models
{
    public class Privacy
    {
        public string Title { get; set; }
        public List<PrivacyContent> Contents { get; set; }
    }

    public class PrivacyContent
    {
        public string MainContent { get; set; }
        public List<PrivacySubContent> SubContents { get; set; }
        public string Link { get; set; }
    }

    public class PrivacySubContent
    {
        public string SubContent { get; set; }
        public List<string> SubItems { get; set;}
    }
}
