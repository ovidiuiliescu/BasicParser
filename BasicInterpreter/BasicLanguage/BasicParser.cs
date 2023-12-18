using CoreParser;

namespace Interpreter.BasicLanguage
{
    internal partial class BasicParser
    {
        public static BasicProgram Parse(string programSource)
            => Parse(new TextWithPointer(programSource));

        public static BasicProgram Parse(TextWithPointer source)
        {
            var program = new BasicProgram(new());

            while (source.Advance())
            {
                // Each program is made up of one or more lines
                ReadNextLineIntoProgram(source, program);
            }

            return program;
        }

        private static void ReadNextLineIntoProgram(TextWithPointer source, BasicProgram program)
        {
            // Each Basic line must start with a line number
            var lineNumber = CoreEntitiesReader.Integer(source);
            var parsedLineNumber = int.Parse(lineNumber);

            // Followed by the actual line content
            var rawLine = Parser.Until(source, ParserExtras.Char.IsEndOfLine);
            var parsedLine = ParseLine(rawLine);

            // Add the line to the program
            program.Lines[parsedLineNumber] = parsedLine;
        }

        private static BasicLine ParseLine(string code)
        {
            // Treat the line as a standalone parsing context
            var source = new TextWithPointer(code);

            // We can have multiple instructions per line
            var instructions = new List<BasicInstruction>();

            while (source.Advance())
            {
                // Each instruction must start with a Basic command
                // Note that not all Basic keywords are commands, e.g. TO
                var basicCommand = ReadBasicCommand(source);

                // Arguments for said Basic command
                var arguments = ReadCommandArguments(source, basicCommand);

                // All good, build a full instruction and add it to the list
                instructions.Add(new BasicInstruction(basicCommand, arguments));
            }

            return new BasicLine(instructions);
        }
        private static BasicCommand ReadBasicCommand(TextWithPointer source)
        {
            // Simple way to map text symbols to commands
            var symbol = CoreEntitiesReader.Symbol(source);
            if (Enum.TryParse<BasicCommand>(symbol, ignoreCase: true, out var command))
            {
                return command;
            }

            throw new ArgumentException($"Unknown basic command: {symbol}");
        }

        private static List<string> ReadCommandArguments(TextWithPointer source, BasicCommand command)
        {
            // Get rid of any trailing whitespace (since this is common to all argument sets)
            ParserExtras.SkipWhiteSpace(source);

            // Parse the correct arguments based on the command type
            var result = command switch
            {
                BasicCommand.Goto => CommandArgumentsReader.Goto(source),
                BasicCommand.Print => CommandArgumentsReader.Print(source),
                BasicCommand.If => CommandArgumentsReader.If(source),
                BasicCommand.Let => CommandArgumentsReader.Let(source),
                BasicCommand.Next => CommandArgumentsReader.Next(source),
                BasicCommand.For => CommandArgumentsReader.For(source),
                BasicCommand unknown => throw new Exception($"Unknown basic command: {unknown}")
            };

            // Same, skip to next instruction. Nothing can follow a list of command arguments,
            // so you can consider this to be part of the syntax of the language.
            // The only exception is the "If" command which has a command IMMEDIATELY following it
            if (command != BasicCommand.If)
            {
                Misc.SkipToEndOfInstruction(source);
            }

            return result;
        }
    }
}
