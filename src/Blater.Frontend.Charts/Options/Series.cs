namespace Blater.Frontend.Charts.Options;

public class Series : List<Serie>;

public record Serie(string Name, List<object> Data);