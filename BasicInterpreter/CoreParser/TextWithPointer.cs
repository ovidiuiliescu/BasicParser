namespace CoreParser
{
    /// <summary>
    /// Defines a text to parse and a pointer in said text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="pointer"></param>
    public class TextWithPointer(string text, int pointer = -1) // Default is -1 to require at least one Advance() call, makes it easier to write loops.
    {
        public string Text { get; init; } = text;

        public int Pointer { get; set; } = pointer;

        public char? Current => Pointer < Text.Length && Pointer > -1 ? Text[Pointer] : null;

        /// <summary>
        /// Advances the pointer and returns true if the new pointer position is valid
        /// </summary>
        /// <returns></returns>
        public bool Advance()
        {
            Pointer++;
            return Current != null;
        }
    }
}
