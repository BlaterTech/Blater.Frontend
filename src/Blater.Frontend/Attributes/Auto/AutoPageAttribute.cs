using Blater.Frontend.Enumerations;

namespace Blater.Frontend.Attributes.Auto;

[AttributeUsage(AttributeTargets.Class)]
public class AutoPageAttribute(
    bool mainPage,
    bool createPage,
    bool editPage,
    bool detailsPage,
    BlaterProjects project)
    : Attribute
{
    public bool MainPage { get; set; } = mainPage;
    public bool EditPage { get; set; } = editPage;
    public bool CreatePage { get; set; } = createPage;
    public bool DetailsPage { get; set; } = detailsPage;
    public BlaterProjects Project { get; } = project;
}