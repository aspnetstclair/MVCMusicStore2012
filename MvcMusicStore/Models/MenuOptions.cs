using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Models
{
    public class MenuOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Artist> Artists { get; set; }
    }
}