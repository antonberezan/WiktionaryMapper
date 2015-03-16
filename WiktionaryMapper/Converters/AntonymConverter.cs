using Memoling.Tools.WiktionaryMapper.Data;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    class AntonymConverter : TermsConverter<Antonym>
    {

        public AntonymConverter(string part) : base(part)
        {
        }

        protected override string Convert(Antonym term)
        {
            return term.ExpressionB;
        }
    }
}