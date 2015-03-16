using System;
using System.IO;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public abstract class OutputWriter : OutputProcessor
    {
        private readonly StreamWriter _writer;
        private readonly bool _closeStream;

        protected OutputWriter(IOutputConverter<Word> converter, StreamWriter writer, bool closeStream = true) : base(converter)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            _writer = writer;
            _closeStream = closeStream;
        }

        protected override void Process(Word word)
        {
            Process(word, _writer);
        }

        protected override void Start()
        {
            Start(_writer);
        }

        protected override void End()
        {
            End(_writer);
            if (_closeStream)
            {
                _writer.Close();
            }
        }

        protected abstract void Process(Word word, StreamWriter writer);
        protected abstract void Start(StreamWriter writer);
        protected abstract void End(StreamWriter writer);
    }
}