using System.Collections.Generic;
using System.Threading;

namespace Memoling.Tools.WiktionaryMapper.Data
{
    public class TranslationsWithMeaning
    {
        private static long _initialId = 0;

        public static long NextId()
        {
            return Interlocked.Increment(ref _initialId);
        }

        public IEnumerable<Translation> Translations { get; set; }
        public string Meaning { get; set; }
        public long MeaningId { get; set; }
    }
}
