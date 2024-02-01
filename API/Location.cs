namespace SearchLocationApi;

public record Location(int Id, string Name, TimeSpan OpeningTime, string BusinessType, TimeSpan ClosingTime);