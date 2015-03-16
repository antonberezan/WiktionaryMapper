using System.Collections.Generic;
using System.Linq;
using Memoling.Tools.WiktionaryParser;
using Memoling.Tools.WiktionaryParser.Data;
using Memoling.Tools.WiktionaryParser.Output;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    class WordConverter : IOutputConverter<Word>
    {
        public IEnumerable<Word> Convert(DataProcessorResult result)
        {
            yield return new Word
            {
                Label = result.Context.Title,
                Language = "en", //todo,
                Etimologies = new EtimologyConverter().Convert(result).ToList()
            };
        }
    }
}