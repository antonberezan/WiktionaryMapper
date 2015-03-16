namespace Memoling.Tools.WiktionaryMapper.Data
{
    public class PartOfSpeech
    {
        public string Expression { get; set; }
        public string Language { get; set; }
        public string Part { get; set; }
        public string Definition { get; set; }

        public override string ToString()
        {
            return (Expression ?? "null") + "|" + (Language ?? "null") + "|" + (Part ?? "null") + "|" + (Definition ?? "null");
        }
    }
}
