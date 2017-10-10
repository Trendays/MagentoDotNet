using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public class MagentoClientConfig
    {
        public bool UseRewritedUrlFormat { get; set; } = true;
        public bool ContentTypeHeaderWithUnderscore { get; set; }

        public string BaseUrl { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
