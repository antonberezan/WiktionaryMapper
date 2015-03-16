using System;
using Memoling.Tools.WiktionaryParser.Data;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public interface IOutputProcessor : IDisposable
    {
        void Process(DataProcessorResult result);
    }
}