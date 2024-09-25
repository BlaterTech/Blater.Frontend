namespace Blater.Frontend.Client.Auto.AutoAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class AutoPageAttribute(
    bool createPage,
    bool createTimelinePage,
    bool editPage,
    bool editTimelinePage,
    bool detailsPage,
    bool detailsTabsPage) : Attribute
{
    public bool CreatePage { get; set; } = createPage;
    public bool CreateTimelinePage { get; set; } = createTimelinePage;
    public bool EditPage { get; set; } = editPage;
    public bool EditTimelinePage { get; set; } = editTimelinePage;
    public bool DetailsPage { get; set; } = detailsPage;
    public bool DetailsTabsPage { get; set; } = detailsTabsPage;
}