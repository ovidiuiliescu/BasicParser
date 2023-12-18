namespace Interpreter.BasicLanguage
{
    internal enum BasicCommand
    {
        Print,
        Goto,
        Let,
        If,
        For,
        Next
    }

    internal class BasicProgram(Dictionary<int, BasicLine> lines)
    {
        public Dictionary<int, BasicLine> Lines { get; init; } = lines;
    }

    internal class BasicLine(List<BasicInstruction> instructions)
    {
        public List<BasicInstruction> Instructions { get; init; } = instructions;
    }

    internal class BasicInstruction(BasicCommand command, List<string> arguments)
    {
        public BasicCommand Command { get; init; } = command;

        public List<string> Arguments { get; init; } = arguments;
    }
}
