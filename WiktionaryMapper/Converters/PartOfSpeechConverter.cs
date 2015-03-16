using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Memoling.Tools.WiktionaryMapper.Data;
using Memoling.Tools.WiktionaryParser;
using Memoling.Tools.WiktionaryParser.Data;
using Memoling.Tools.WiktionaryParser.Output;

namespace Memoling.Tools.WiktionaryMapper.Converters
{
    /// <summary>
    /// Part of speech render.
    /// </summary>
    internal class PartOfSpeechConverter : OutputConverterBase<Part, PartOfSpeech>
    {
        private readonly string _etimology;

        public PartOfSpeechConverter(string etimology)
        {
            _etimology = etimology;
        }

        protected override Part Convert(DataProcessorResult result, PartOfSpeech part)
        {
            return new Part
            {
                Type = part.Part,
                Definitions = GetDefinitons(part.Definition).ToList(),
                Synonyms = new SynonymConverter(part.Part).Convert(result).ToList(),
                Antonyms = new AntonymConverter(part.Part).Convert(result).ToList()
            };
        }

        protected override bool Filter(Section section)
        {
            return section.Parent == _etimology;
        }

        private static IEnumerable<Definition> GetDefinitons(string content)
        {
            var lines = content.Replace("\n|", "|").Split('\n').ToList();

            const string prefix = "#";

            while (lines.Count > 0)
            {
                var line = lines.First();
                if (line.StartsWith(prefix))
                {
                    var definition = GetDefinition(prefix, lines);
                    if (definition != null)
                    {
                        yield return definition;
                    }
                }
                lines.Remove(line);
            }
        }

        private static readonly Regex DefinitionReg = new Regex(@"\#{1,}[:*]?\s?(?<attributes>\{.*\})?\s?(?<definition>[^{}].*)?",
            RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static Definition GetDefinition(string prefix, ICollection<string> lines)
        {
            var definition = new Definition
            {
                Attributes = new Collection<string>(),
                Definitions = new Collection<Definition>()
            };

            while (lines.Count > 0)
            {
                var line = lines.First();

                if (line.StartsWith(prefix + ":"))
                {
                    definition.Examples = GetExamples(prefix, lines).ToList();
                }
                else if (line.StartsWith(prefix + "*"))
                {
                    
                    definition.Quotes = GetQuotes(prefix, lines).ToList();
                }
                else if (line.StartsWith(prefix + "#"))
                {
                    definition.Definitions.Add(GetDefinition(prefix + "#", lines));
                }
                else if (definition.Value == null && line.StartsWith(prefix))
                {
                    var match = DefinitionReg.Match(line);
                    definition.Value = match.Groups["definition"].Value;
                    if (string.IsNullOrWhiteSpace(definition.Value))
                    {
                        return null;
                    }
                    if (match.Groups["attributes"].Success)
                    {
                        definition.Attributes.Add(match.Groups["attributes"].Value);
                    }
                }
                else if (line.StartsWith("|"))
                {

                }
                else
                {
                    break;
                }
                lines.Remove(line);
            }
            return definition;
        }

        private static readonly Regex ExampleReg = new Regex(@"\#{1,}:\s?\{{1,2}(?:\w*?\|){0,}(?<example>[^\|\}]*).*\}",RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static IEnumerable<string> GetExamples(string prefix, ICollection<string> lines)
        {
            while (lines.Count > 0 && lines.First().StartsWith(prefix + ":"))
            {
                var line = lines.First();
                var match = ExampleReg.Match(line);

                yield return match.Success
                    ? match.Groups["example"].Value
                    : line.Replace(prefix + ":", "").Trim().Trim('\'', '\'');

                lines.Remove(line);
            }
        }

        private static readonly Regex QuoteBookReg = new Regex(@"\#{1,}\*\s?\{\{.*?(?:author=(?<author>[^\|]*))?.*?(?:passage=(?<quote>[^\|]*))?\}\}", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex QuoteAuthorReg = new Regex(@"\#{1,}\*\s?[\{\[]{1,2}(?:w[\|:])?(?<author>.*?)[\}\]]{1,2}", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex QuoteReg = new Regex(@"\#{1,}\*\s?''(?<quote>.*?('''.*?''').*?)''", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static IEnumerable<Quote> GetQuotes(string prefix, ICollection<string> lines)
        {
            while (lines.Count > 0 && lines.First().StartsWith(prefix + "*"))
            {
                var line = lines.First();
                
                var bookQuote = QuoteBookReg.Match(line);
                if (bookQuote.Success)
                {
                    yield return new Quote
                    {
                        Author = bookQuote.Groups["author"].Success ? bookQuote.Groups["author"].Value : null,
                        Quotes = bookQuote.Groups["quote"].Success ? new List<string> { bookQuote.Groups["quote"].Value } : null
                    };
                    lines.Remove(line);
                    continue;
                }

                var simpleQuote = QuoteReg.Match(line);
                if (simpleQuote.Groups["quote"].Success)
                {
                    yield return new Quote
                    {
                        Quotes = new List<string> { simpleQuote.Groups["quote"].Value }
                    };
                    lines.Remove(line);
                    continue;
                }

                var authorQuote = QuoteAuthorReg.Match(line);
                string author = null;
                if (authorQuote.Success)
                {
                    author = authorQuote.Groups["author"].Value;
                }
                else if (!line.StartsWith(prefix + "*:"))
                {
                    author = line.Replace(prefix + "*", "").Trim().Trim('\'', '\'');
                }

                lines.Remove(line);
                yield return new Quote
                {
                    Author = author,
                    Quotes = GetExamples(prefix + "*", lines).ToList()
                };
            }
        }
    }
}