# A very simple parser combinator

Parsing structured text (e.g. program source code) sounds more complicated than it actually is.

This repo exemplifies how to quickly write a [parser combinator](https://en.wikipedia.org/wiki/Parser_combinator) that can be used to do just that. See also [this video](https://www.youtube.com/watch?v=dDtZLm7HIJs) for a quick overview of what we're trying to achieve.

Design goals:

 - Simple code, minimal comments (let the code do the talking)
 - Clear separation of concerns between classes
 - Easy to adapt to your own needs (core parser functionality is **less than 100 lines**, can be reused in you own projects)
 - Meant to be evocative, not exhaustive (things like error handling are greatly simplified)
 - 100% working, but intentionally incomplete (so you can have fun extending the code and tinkering with it).

## What can it do
The code in this repo is able to parse somewhat complex [BASIC programs](https://en.wikipedia.org/wiki/BASIC) such as:
```
10 LET SomeVariable = "John" + " " + "Smith"
20 PRINT "Hello, ", SomeVariable ,"! How are you?" : PRINT "This is another print statement." : LET SomeNumber123 = 37  + 1 * (4 * 5)
30 FOR ALoopVariable = 37 + SomeNumber123 To 1000
40 LET Temp = ALoopVariable MOD 5
50 IF ALoopVariable > 400 AND Temp = 2 THEN PRINT "This is a conditional print: ", ALoopVariable
60 NEXT
```

Feel free to play around and add support for more BASIC features, such as:

 - User input via the`INPUT` command  (should be similar to how the `PRINT` command is already parsed)
 - User functions via `DEF FN`(how would you declare and parse user arguments in this case?)
 - Actually running the parsed BASIC programs (the parser returns a mostly-usable [AST](https://en.wikipedia.org/wiki/Abstract_syntax_tree), but expression evaluation, not to be confused with expression parsing which is already handled, needs to be carefully thought out)

# What's in the box

This repo takes the form of a single Visual Studio Solution. The solution itself is split in two parts:
* A universal parser combinator. Simple, concise, and portable. Can be reused in your own projects if you want to. Not meant to be feature complete.
* A BASIC program parser. Uses the universal parser functionality to parse actual BASIC programs.

The relevant files and their descriptions are as follows:

## Universal parser functionality

|File|Description|
|--|--|
|Parser.cs| Core parser functionality (`While()`,`Until()`) and combinator (`Union()`,`Optional()`) |
|ParserExtras.cs| Non-core but widely used parser helpers|
|TextWithPointer.cs| Represents source code with a "current pointer"|


## BASIC language parser functionality
These files use the universal parser functionality mentioned previously to implement a simple BASIC program parser.

|File|Description|
|--|--|
|BasicParser.cs| Used as a high-level wrapper/parser over a BASIC program |
|BasicProgramEntities.cs| Entities used in `BasicParser.cs`|
|CommandArgumentsReader.cs| Parser for BASIC command arguments (e.g. PRINT, FOR, LET, etc)|
|CoreEntitiesReader.cs| Parsers for BASIC entities like string, int, etc|
|ExpressionReader.cs| Parser for BASIC math-like expressions|
|ListReader.cs| Parser for BASIC lists, in particular lists of command arguments|
|Misc.cs| Various helper functions|

