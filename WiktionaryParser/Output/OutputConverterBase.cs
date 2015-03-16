using System.Collections.Generic;
using System.Linq;
using Memoling.Tools.WiktionaryParser.Data;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public abstract class OutputConverterBase<TResult, TContent> : IOutputConverter<TResult> where TContent : class
    {
        protected abstract TResult Convert(DataProcessorResult result, TContent content);

        public IEnumerable<TResult> Convert(DataProcessorResult result)
        {
            var contents = result.TransformedSections.Where(Filter).SelectMany<dynamic, TContent>(s => GetContents(s.Content));
            return contents.Select(c => Convert(result, c));
        }

        protected virtual bool Filter(Data.Section section)
        {
            return true;
        }

        private static IEnumerable<TContent> GetContents(dynamic content)
        {
            var contents = content as IEnumerable<TContent>;

            if (contents != null)
            {
                foreach (var item in contents.SelectMany(GetContents))
                {
                    yield return item;
                }
            }

            var result = content as TContent;
            if (result != null)
            {
                yield return result;
            }
        }
    }
}