using System.Collections.Generic;

namespace Memoling.Tools.WiktionaryParser
{
    public class Word
    {
        public string Label { get; set; }

        public string Language { get; set; }

        public IList<Etimology> Etimologies { get; set; } 
    }
}