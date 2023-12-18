using CoreParser;

namespace Interpreter.BasicLanguage
{
    public static class CommandArgumentsReader
    {
        public static List<string> Print(TextWithPointer source)
        {
            // Print arguments are just a list of expression
            return ListReader.ListOfExpressions(source);
        }

        public static List<string> Goto(TextWithPointer source)
        {
            // Argument for GOTO is just a line number
            return new List<string> { CoreEntitiesReader.Integer(source) };
        }

        public static List<string> Next(TextWithPointer source)
        {
            // Next has no arguments
            return new List<string> { };
        }

        public static List<string> If(TextWithPointer source)
        {
            // We just care about the condition here.
            var condition = ExpressionReader.GenericExpression(source);

            // Skip the "then". Don't read the whitespace after it, leave it for the next command to advance it.
            // Otherwise you may end up "swallowing" the first letter of the next command.
            ParserExtras.SkipWhiteSpace(source);
            Parser.While(source, ParserExtras.BuildWhilePredicateForWord("then"));

            return new List<string> { condition };
        }

        public static List<string> For(TextWithPointer source)
        {
            // Variable name
            var variableName = CoreEntitiesReader.Symbol(source);

            // Skip the mandatory "="
            ParserExtras.SkipMandatoryTextWithWhiteSpace(source, Char.IsEqual);

            // Read the start value for the loop
            var start = ExpressionReader.GenericExpression(source);

            // Skip the mandatory "To"
            ParserExtras.SkipMandatoryTextWithWhiteSpace(source, ParserExtras.BuildWhilePredicateForWord("to"));

            // Read the end value for the loop
            var end = ExpressionReader.GenericExpression(source);

            return new List<string> { variableName, start, end };
        }

        public static List<string> Let(TextWithPointer source)
        {
            // Read the variable name
            var variableName = CoreEntitiesReader.Symbol(source);

            // Skip the mandatory "="
            ParserExtras.SkipMandatoryTextWithWhiteSpace(source, Char.IsEqual);

            // Read the expression to assign
            var expression = ExpressionReader.GenericExpression(source);

            return new List<string> { variableName, expression };
        }

    }
}
