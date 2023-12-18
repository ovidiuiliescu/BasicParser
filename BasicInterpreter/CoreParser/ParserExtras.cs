namespace CoreParser
{
    /// <summary>
    /// Mostly convenience methods that can be used with the <see cref="Parser"/> class. Not critical to writing parsers, but useful
    /// to prevent reinventing the wheel in many cases.
    /// </summary>
    public static class ParserExtras
    {
        public static class Char
        {
            public static bool IsWhiteSpace(char? c) => c is char actualChar && char.IsWhiteSpace(actualChar);

            public static bool IsDoubleQuote(char? c) => c == '"';

            public static bool IsEndOfLine(char? c) => c == null || c == '\r' || c == '\n';

            public static bool IsDigit(char? c) => c is char actualChar && char.IsDigit(actualChar);

            public static bool IsLetter(char? c) => c is char actualChar && char.IsLetter(actualChar);
        }

        public static void SkipOptionalText(TextWithPointer text, Predicate<char?> whilePredicate)
            => Parser.Optional(text, p => Parser.While(p, whilePredicate));

        public static void SkipWhiteSpace(TextWithPointer text)
            => SkipOptionalText(text, Char.IsWhiteSpace);

        public static string SkipMandatoryTextWithWhiteSpace(TextWithPointer text, Predicate<char?> whilePredicate)
        {
            SkipWhiteSpace(text);
            var result = Parser.While(text, whilePredicate);
            SkipWhiteSpace(text);

            return result;
        }

        public static Predicate<char?> BuildWhilePredicateForWord(string word)
        {
            int index = -1;
            return Match;

            bool Match(char? c)
            {
                index++;
                return index < word.Length && c is char actualChar && char.ToUpperInvariant(word[index]) == char.ToUpperInvariant(actualChar);
            }
        }
    }
}
