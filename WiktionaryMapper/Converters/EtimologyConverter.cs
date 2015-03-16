using System.Collections.Generic;
using System.Linq;
using Memoling.Tools.WiktionaryMapper.Data;
using Memoling.Tools.WiktionaryParser;
using Memoling.Tools.WiktionaryParser.Data;
using Memoling.Tools.WiktionaryParser.Output;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    public class EtimologyConverter : IOutputConverter<Etimology>
    {
        public IEnumerable<Etimology> Convert(DataProcessorResult result)
        {
            var groups = result.TransformedSections.Where(IsPartOfSpeech).GroupBy(s => s.Parent);

            return groups.Select(g => new Etimology
            {
                Parts = new PartOfSpeechConverter(g.Key).Convert(result).ToList()
            });
        }

        private static bool IsPartOfSpeech(Section section)
        {
            return section.Content is IEnumerable<PartOfSpeech> || section.Content is PartOfSpeech;
        }
    }
}