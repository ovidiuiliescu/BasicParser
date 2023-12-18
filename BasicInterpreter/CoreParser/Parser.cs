using System.Text;

namespace CoreParser
{
    public static class Parser
    {
        public static string While(TextWithPointer text, Predicate<char?> continueParsingPredicate)
        {
            var buffer = new StringBuilder();

            while (continueParsingPredicate(text.Current))
            {
                buffer.Append(text.Current);

                if (!text.Advance())
                {
                    break;
                }
            }

            if (buffer.Length == 0)
            {
                // To allow empty input, you can use Parse.Optional
                throw new ArgumentException($"Nothing read from input");
            }

            return buffer.ToString();
        }

        public static string Until(TextWithPointer text, Predicate<char?> stopConditionPredicate)
            => While(text, c => !stopConditionPredicate(c));

        public static string Union(TextWithPointer text, params Func<TextWithPointer, string>[] predicates)
        {
            var originalPosition = text.Pointer;

            foreach (var predicate in predicates)
            {
                try
                {
                    return predicate(text);
                }
                catch
                {
                    text.Pointer = originalPosition;
                }
            }

            throw new ArgumentException($"Unable to read from position {text.Pointer}");
        }

        public static string Optional(TextWithPointer text, params Func<TextWithPointer, string>[] predicates)
        {
            try
            {
                return Union(text, predicates);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
