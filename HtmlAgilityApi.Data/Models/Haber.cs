using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HtmlAgilityApi.Data.Models
{
    public class Haber
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string NewsLink { get; set; }
        public int NewsPhotoId { get; set; }
        public string NewsPhotoLink { get; set; }

    }
}