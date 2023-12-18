using CoreParser;
using System.Text;

namespace Interpreter.BasicLanguage
{
    public static class ExpressionReader
    {
        public static string GenericExpression(TextWithPointer source)
        {
            var result = new StringBuilder();

            // We must have at least one operand
            result.Append(ReadOperand(source));

            // If we have an operator, then recursively read the rest of the expression
            var op = Parser.Optional(source, ReadOperator);
            if (op.Length > 0)
            {
                result.Append($" {op} ");
                result.Append(GenericExpression(source));
            }

            return result.ToString();
        }

        public static string ReadOperator(TextWithPointer source)
        {
            ParserExtras.SkipWhiteSpace(source);
            var op = Parser.Union(source, ReadPlus, ReadMinus, ReadMultiply, ReadDivide, ReadMod,
                ReadLessThanOrEqual, ReadGreaterThanOrEqual, ReadAnd, ReadOr, ReadGreaterThan, ReadEquals, ReadLessThan);
            ParserExtras.SkipWhiteSpace(source);

            return op;
        }

        public static string ReadOperand(TextWithPointer source)
        {
            ParserExtras.SkipWhiteSpace(source);
            var op = Parser.Union(source, CoreEntitiesReader.Integer, CoreEntitiesReader.StringLiteral, CoreEntitiesReader.Symbol, ReadParanthesis);
            ParserExtras.SkipWhiteSpace(source);

            return op;
        }

        public static string ReadParanthesis(TextWithPointer source)
        {
            var result = new StringBuilder();

            result.Append(ParserExtras.SkipMandatoryTextWithWhiteSpace(source, ParserExtras.BuildWhilePredicateForWord("(")));
            result.Append(GenericExpression(source));
            result.Append(ParserExtras.SkipMandatoryTextWithWhiteSpace(source, ParserExtras.BuildWhilePredicateForWord(")")));

            return result.ToString();
        }

        public static string ReadPlus(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("+"));

        public static string ReadMinus(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("-"));

        public static string ReadMultiply(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("*"));

        public static string ReadDivide(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("/"));
        public static string ReadMod(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("mod"));

        public static string ReadEquals(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("="));

        public static string ReadGreaterThan(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord(">"));

        public static string ReadLessThan(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("<"));

        public static string ReadLessThanOrEqual(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("<="));

        public static string ReadGreaterThanOrEqual(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord(">="));

        public static string ReadAnd(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("and"));

        public static string ReadOr(TextWithPointer source)
            => Parser.While(source, ParserExtras.BuildWhilePredicateForWord("or"));


    }
}
