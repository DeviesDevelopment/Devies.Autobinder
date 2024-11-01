using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Devies.Autobinder
{

    [Generator]
    public class AutobinderGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {

            context.RegisterPostInitializationOutput(static postInitializationContext =>
            postInitializationContext.AddSource("Devies.Autobinder.Attribute.Generated.cs", SourceText.From("""
                using System;
                namespace Devies.Autobinder {
                    [AttributeUsage(AttributeTargets.Parameter)]
                    public class AutobindAttribute: Attribute {
                        public required string ParamName { get; set; }
                    }
                }
                """, Encoding.UTF8)));
                
                var pipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                    fullyQualifiedMetadataName: "Devies.Autobinder.Autobind",
                    predicate: static (syntaxNode, cancellationToken) => syntaxNode is BaseMethodDeclarationSyntax,
                    transform: static (context, cancellationToken) =>
                    {
                        var containingClass = context.TargetSymbol.ContainingType;
                        return new Model(
                            // Note: this is a simplified example. You will also need to handle the case where the type is in a global namespace, nested, etc.
                            @namespace: containingClass.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted)),
                            className: containingClass.Name,
                            methodName: context.TargetSymbol.Name);
                    }
            );

            context.RegisterSourceOutput(pipeline, static (context, model) =>
            {
                var sourceText = SourceText.From($$"""
                    namespace {{model.Namespace}};
                    partial class {{model.ClassName}}
                    {
                        partial void {{model.MethodName}}()
                        {
                            // generated code
                        }
                    }
                    """, Encoding.UTF8);

                context.AddSource($"{model.ClassName}_{model.MethodName}.g.cs", sourceText);
            });
        }
    }


    public record Model {
        public Model(string @namespace, string className, string methodName) {
            Namespace = @namespace;
            ClassName = className;
            MethodName = methodName;
        }

        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}