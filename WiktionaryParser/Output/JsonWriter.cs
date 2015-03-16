using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Memoling.Tools.WiktionaryParser.Output
{
    public class JsonWriter : OutputWriter
    {
//        private readonly IList<Word> _words = new List<Word>();
        private readonly JsonSerializerSettings _settings;

        public JsonWriter(IOutputConverter<Word> converter, StreamWriter writer, bool closeStream = true) 
            : base(converter, writer, closeStream)
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        protected override void Process(Word word, StreamWriter writer)
        {
//            _words.Add(word);
            writer.Write(Environment.NewLine + JsonConvert.SerializeObject(word, Formatting.Indented, _settings) + ",");
        }

        protected override void Start(StreamWriter writer)
        {
            writer.Write("[");
        }

        protected override void End(StreamWriter writer)
        {
            writer.Write("]");
        }
    }
}