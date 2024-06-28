using System.Diagnostics;
using Blater.Frontend.SourceGenerator.Templates;
using Microsoft.CodeAnalysis;

namespace Blater.Frontend.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class AutoPageGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, GenerateSource);
    }

    private void GenerateSource(SourceProductionContext context, Compilation compilation)
    {
        // Same workflow as Execute method in ISourceGenerator
        var autoSystemAssemblies = compilation.SourceModule.ReferencedAssemblySymbols
                                              .Where(x => x.Name.StartsWith("AutoSystem", StringComparison.OrdinalIgnoreCase));

        //Add self assembly
        autoSystemAssemblies = autoSystemAssemblies.Append(compilation.Assembly);

        var allNamespaces = autoSystemAssemblies.SelectMany(x => x.GlobalNamespace.GetAllNamespaces()).ToList();
        var modelsNameSpaces = allNamespaces
           .Where(x => x.Name.Contains("Contracts"));

        var members = modelsNameSpaces.SelectMany(x => x.GetAllMembers()).ToList();

        var pageModels = members
           .Where(x => x.GetAttributes().Any(att => att.AttributeClass?.Name == "AutoPageAttribute"));

        var projectNamespace = compilation.AssemblyName!;

        var namespaces = members.Select(x => x.ContainingNamespace.ToString()).Distinct().ToList();

        foreach (var pageModel in pageModels)
        {
            var autoPageAttribute = pageModel.GetAttributes().First(x => x.AttributeClass?.Name == "AutoPageAttribute");
            var autoPageAttributeArguments = autoPageAttribute.ConstructorArguments;
            var mainPage = (bool)autoPageAttributeArguments[0].Value!;
            var createPage = (bool)autoPageAttributeArguments[1].Value!;
            var editPage = (bool)autoPageAttributeArguments[2].Value!;
            var detailsPage = (bool)autoPageAttributeArguments[3].Value!;
            //var modules = autoPageAttributeArguments[4].Values.Select(x => x.Value?.ToString());

            /*if (mainPage)
            {
                context.AddSource($"{pageModel.Name}MainPage.g.razor", MainPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));
            }*/

            if (createPage)
            {
                var sourceFileName = $"{pageModel.Name}CreatePage.g.razor";
                context.AddSource(sourceFileName,
                                  CreatePageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (editPage)
            {
                var sourceFileName = $"{pageModel.Name}EditPage.g.razor";
                context.AddSource(sourceFileName,
                                  EditPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (detailsPage)
            {
                var sourceFileName = $"{pageModel.Name}DetailsPage.g.razor";
                context.AddSource(sourceFileName,
                                  DetailsPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }
        }

        Debug.WriteLine("AutoPageGenerator finished!");
    }
}