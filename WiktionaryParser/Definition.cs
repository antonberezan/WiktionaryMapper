using System.Collections.Generic;

namespace Memoling.Tools.WiktionaryParser
{
    public class Definition
    {
        public string Value { get; set; }

        public ICollection<string> Examples { get; set; }

        public ICollection<Quote> Quotes { get; set; }

        public ICollection<string> Attributes { get; set; } 
        
        public ICollection<Definition> Definitions { get; set; }

        public bool ShouldSerializeExamples()
        {
            return Examples != null && Examples.Count > 0;
        }

        public bool ShouldSerializeQuotes()
        {
            return Quotes != null && Quotes.Count > 0;
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes != null && Attributes.Count > 0;
        }

        public bool ShouldSerializeDefinitions()
        {
            return Definitions != null && Definitions.Count > 0;
        }
    }
}