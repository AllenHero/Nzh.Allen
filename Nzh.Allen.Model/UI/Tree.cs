using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    public class Tree
    {
        public int id { get; set; }

        public string title { get; set; }

        public string href { get; set; }

        public string fontFamily { get; set; }

        public string icon { get; set; }

        public IEnumerable<Tree> children { get; set; }
    }
}
