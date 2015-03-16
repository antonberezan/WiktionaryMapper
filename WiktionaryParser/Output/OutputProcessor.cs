using System;
using System.Linq;
using Memoling.Tools.WiktionaryParser.Data;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public abstract class OutputProcessor : IOutputProcessor
    {
        private readonly IOutputConverter<Word> _converter;
        private bool _start;

        protected OutputProcessor(IOutputConverter<Word> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            _converter = converter;
            _start = true;
        }

        public void Process(DataProcessorResult result)
        {
            if (_start)
            {
                Start();
                _start = false;
            }
            var word = _converter.Convert(result).FirstOrDefault();
            if (word != null)
            {
                Process(word);
            }
        }

        protected abstract void Start();
        protected abstract void Process(Word word);
        protected abstract void End();

        #region IDisposable implementation

        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    End();
                    // TODO: dispose managed state (managed objects).          
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}