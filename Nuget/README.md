Peppermill is a fork of [CSharpier](https://github.com/belav/csharpier), an opinionated code formatter for c#. It uses Roslyn to parse your code and re-prints it using its own rules.

### Quick Start
Install Peppermill globally using the following command.
```bash
dotnet tool install peppermill -g
```
Then format the contents of a directory and its children with the following command.
```bash
peppermill .
```
