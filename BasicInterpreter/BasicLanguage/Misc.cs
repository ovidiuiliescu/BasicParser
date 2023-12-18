using CoreParser;

namespace Interpreter.BasicLanguage
{
    public static class Misc
    {
        public static void SkipToEndOfInstruction(TextWithPointer source)
        {
            ParserExtras.SkipWhiteSpace(source);

            if (source.Current is not null && !Char.IsEndOfInstruction(source.Current))
            {
                throw new ArgumentException("Invalid syntax");
            }
        }
    }

    public static class Char
    {
        public static bool IsEndOfInstruction(char? c) => c == ':' || ParserExtras.Char.IsEndOfLine(c);

        public static bool IsListSeparator(char? c) => c == ',';

        public static bool IsEqual(char? c) => c == '=';
    }
}
