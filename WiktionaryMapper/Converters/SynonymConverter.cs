using Memoling.Tools.WiktionaryMapper.Data;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    class SynonymConverter : TermsConverter<Synonym> {

        public SynonymConverter(string part) : base(part)
        {
        }

        protected override string Convert(Synonym term)
        {
            return term.ExpressionB;
        }
    }
}