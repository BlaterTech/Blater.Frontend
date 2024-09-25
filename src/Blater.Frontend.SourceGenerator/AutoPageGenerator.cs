using System.Diagnostics;
using Blater.Frontend.SourceGenerator.Templates.Types.Create;
using Blater.Frontend.SourceGenerator.Templates.Types.Details;
using Blater.Frontend.SourceGenerator.Templates.Types.Edit;
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
        var blaterAssemblies = compilation
                              .SourceModule
                              .ReferencedAssemblySymbols
                              .Where(x => x.Name.StartsWith("Blater", StringComparison.OrdinalIgnoreCase));

        //Add self assembly
        blaterAssemblies = blaterAssemblies.Append(compilation.Assembly);

        var allNamespaces = blaterAssemblies
           .SelectMany(x => x.GlobalNamespace.GetAllNamespaces());

        var modelsNameSpaces = allNamespaces
                              .Where(x => x.Name.Contains("Contracts"));

        var members = modelsNameSpaces
                     .SelectMany(x => x.GetAllMembers())
                     .ToList();

        var pageModels = members
                        .Where(x => x
                                   .GetAttributes()
                                   .Any(att => att.AttributeClass?.Name == "AutoPageAttribute"))
                        .ToList();

        var projectNamespace = compilation.AssemblyName!;

        var namespaces = members
                        .Select(x => x.ContainingNamespace.ToString())
                        .Distinct()
                        .ToList();

        foreach (var pageModel in pageModels)
        {
            var autoPageAttribute = pageModel
                                   .GetAttributes()
                                   .FirstOrDefault(x => x.AttributeClass?.Name == "AutoPageAttribute");
            if (autoPageAttribute == null)
            {
                continue;
            }

            var autoPageAttributeArguments = autoPageAttribute.ConstructorArguments;
            var createPage = (bool)autoPageAttributeArguments[0].Value!;
            var createTimelinePage = (bool)autoPageAttributeArguments[1].Value!;
            var editPage = (bool)autoPageAttributeArguments[2].Value!;
            var editTimelinePage = (bool)autoPageAttributeArguments[3].Value!;
            var detailsPage = (bool)autoPageAttributeArguments[4].Value!;
            var detailsTabsPage = (bool)autoPageAttributeArguments[5].Value!;

            if (createPage)
            {
                var sourceFileName = $"{pageModel.Name}CreatePage.g.razor";
                context.AddSource(sourceFileName,
                                  CreatePageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (createTimelinePage)
            {
                var sourceFileName = $"{pageModel.Name}CreateTimelinePage.g.razor";
                context.AddSource(sourceFileName,
                                  CreateTimelinePageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (editPage)
            {
                var sourceFileName = $"{pageModel.Name}EditPage.g.razor";
                context.AddSource(sourceFileName,
                                  EditPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (editTimelinePage)
            {
                var sourceFileName = $"{pageModel.Name}EditTimelinePage.g.razor";
                context.AddSource(sourceFileName,
                                  EditTimelinePageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (detailsPage)
            {
                var sourceFileName = $"{pageModel.Name}DetailsPage.g.razor";
                context.AddSource(sourceFileName,
                                  DetailsPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }

            if (detailsTabsPage)
            {
                var sourceFileName = $"{pageModel.Name}DetailsTabsPage.g.razor";
                context.AddSource(sourceFileName,
                                  DetailsTabsPageTemplate.GetCode(pageModel.Name, projectNamespace, namespaces));

                Debug.WriteLine($"AutoPageGenerator added {sourceFileName}");
            }
        }

        Debug.WriteLine("AutoPageGenerator finished!");
    }
}