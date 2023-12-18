using CoreParser;

namespace Interpreter.BasicLanguage
{
    public static class ListReader
    {
        public static List<string> ListOfExpressions(TextWithPointer source)
        {
            var arguments = new List<string>();

            while (true)
            {
                // Read the next argument.
                var nextArgument = ExpressionReader.GenericExpression(source);
                arguments.Add(nextArgument);

                // Break for anything that isn't a list separator (this includes end of text,
                // instructions separators, new lines, etc)
                if (!Char.IsListSeparator(source.Current))
                {
                    break;
                }

                // Otherwise, skip the separator and go through the loop to read the next argument
                ParserExtras.SkipOptionalText(source, Char.IsListSeparator);
            }

            return arguments;
        }
    }
}
