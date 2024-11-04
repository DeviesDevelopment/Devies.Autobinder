### Pitfalls

#### Run `dotnet add package Microsoft.Net.Compilers.Toolset`
This is not mentioned anywhere. But if you are trying to build a consumer project outside of Visual Studio, especially outside of windows, you need to reference `Microsoft.Net.Compilers.Toolset`.
Otherwise the references to the generated code will result in a compilation error.

#### Needs `.NET Compiler Platform SDK`
Even on windows with VS, without `.NET Compiler Platform SDK` installed, generators won't work. Install it trough the `Visual Studio installer`