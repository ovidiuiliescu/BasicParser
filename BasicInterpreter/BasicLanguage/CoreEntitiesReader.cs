using CoreParser;

namespace Interpreter.BasicLanguage
{
    public static class CoreEntitiesReader
    {
        public static string StringLiteral(TextWithPointer source)
        {
            // You can perform some extra validation here, e.g. double quotes and escape characters.

            ParserExtras.SkipWhiteSpace(source);
            var startQuote = Parser.While(source, ParserExtras.Char.IsDoubleQuote);
            var str = Parser.Until(source, ParserExtras.Char.IsDoubleQuote);
            var endQuote = Parser.While(source, ParserExtras.Char.IsDoubleQuote);
            ParserExtras.SkipWhiteSpace(source);

            return startQuote + str + endQuote;
        }

        public static string Integer(TextWithPointer source)
        {
            ParserExtras.SkipWhiteSpace(source);
            var number = Parser.While(source, ParserExtras.Char.IsDigit);
            ParserExtras.SkipWhiteSpace(source);

            return number;
        }

        public static string Symbol(TextWithPointer source)
        {
            ParserExtras.SkipWhiteSpace(source);
            var symbolName = SymbolIdentifier(source);
            ParserExtras.SkipWhiteSpace(source);

            return symbolName;
        }

        public static string SymbolIdentifier(TextWithPointer source)
        {
            // Symbols must begin with a letter but can also contain numbers.
            // We use recursion to alternate between reading letters and digits until
            // we can read neither, at which point we have our symbol identifier.

            var firstPart = Parser.While(source, ParserExtras.Char.IsLetter);
            var secondPart = Parser.Optional(source, p => Parser.While(p, ParserExtras.Char.IsDigit));
            return firstPart + secondPart + Parser.Optional(source, SymbolIdentifier);
        }
    }
}
