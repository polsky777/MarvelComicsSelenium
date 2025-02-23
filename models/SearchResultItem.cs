using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelSelenium.models
{
    public class SearchResultItem
    {
        public string ContentType { get; set; }
        public string ContentText { get; set; }

        public SearchResultItem(string contentType, string contentText)
        {
            ContentType = contentType;
            ContentText = contentText;
        }
    }
}
