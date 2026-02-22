# Claude Code Instructions

## Token Management
- When working with test files (especially `.test` files), NEVER read the whole test file. Instead, only look at the diff output to understand what needs to change.
- Use `diff` or `--check` output to identify formatting differences rather than reading large source files in full.
- Minimize context window usage by being surgical: read only the specific lines/sections relevant to the current problem.

## Formatting Tests
- Test files in `Src/CSharpier.Tests/FormattingTests/TestFiles/cs/` serve as both input and expected output unless a `.expected.test` file exists.
- To run a single test, copy the `.test` file as a `.cs` file and use `dotnet run --project Src/CSharpier.Cli/CSharpier.Cli.csproj --framework net9.0 -- check <file>` or `format <file>`, then diff against the original.
- Focus on the diff output to understand what the formatter does differently, then make targeted changes.

## Teams & Subagents
- Your context is expensive. Make sure to smartly use Haiku and sonnet subagents for tasks that do not require full cognitive capabilities. 
