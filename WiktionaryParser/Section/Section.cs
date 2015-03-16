﻿
namespace Memoling.Tools.WiktionaryParser.Data
{
    public class Section
    {
        public string Parent { get; set; }
        public string Header { get; set; }
        public dynamic Content { get; set; }

        public override string ToString()
        {
            return (Header ?? "null") + "|" + (Content ?? "null");
        }
    }
}
