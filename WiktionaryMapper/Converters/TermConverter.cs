using Memoling.Tools.WiktionaryParser.Data;
using Memoling.Tools.WiktionaryParser.Output;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    abstract class TermsConverter<TTerm> : OutputConverterBase<string, TTerm> where TTerm : class
    {
        private readonly string _part;

        protected TermsConverter(string part)
        {
            _part = part;
        }

        protected abstract string Convert(TTerm term);

        protected override string Convert(DataProcessorResult result, TTerm term)
        {
            return Convert(term);
        }

        protected override bool Filter(Section section)
        {
            return section.Parent == _part;
        }
    }
}