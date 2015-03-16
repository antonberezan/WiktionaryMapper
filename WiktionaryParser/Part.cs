using System.Collections.Generic;
using Newtonsoft.Json;

namespace Memoling.Tools.WiktionaryParser
{
    public class Part
    {
        public string Type { get; set; }

        public ICollection<Definition> Definitions { get; set; }

        public ICollection<string> Synonyms { get; set; }

        public ICollection<string> Antonyms { get; set; }

        public bool ShouldSerializeDefinitions()
        {
            return Definitions != null && Definitions.Count > 0;
        }

        public bool ShouldSerializeSynonyms()
        {
            return Synonyms != null && Synonyms.Count > 0;
        }

        public bool ShouldSerializeAntonyms()
        {
            return Antonyms != null && Antonyms.Count > 0;
        }
    }
}