using Interpreter.BasicLanguage;

namespace BasicInterpreter
{
    internal class Program
    {
        private static string _sampleBasicProgram = """
        10 LET SomeVariable = "John" + " " + "Smith"
        20 PRINT "Hello, ", SomeVariable ,"! How are you?" : PRINT "This is another print statement." : LET SomeNumber123 = 37  + 1 * (4 * 5)
        30 FOR ALoopVariable = 37 + SomeNumber123 To 1000
        40 LET Temp = ALoopVariable MOD 5
        50 IF ALoopVariable > 400 AND Temp = 2 THEN PRINT "This is a conditional print: ", ALoopVariable
        60 NEXT
        """;

        static void Main(string[] args)
        {
            var program = BasicParser.Parse(_sampleBasicProgram);
            Console.WriteLine($"Parsed {program.Lines.Count} program lines.");
        }
    }
}
