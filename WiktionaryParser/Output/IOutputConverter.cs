using System.Collections.Generic;
using Memoling.Tools.WiktionaryParser.Data;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public interface IOutputConverter<out TResult>
    {
        IEnumerable<TResult> Convert(DataProcessorResult result);
    }
}