namespace MAFTravelPlanner.Contracts.TravelAdvisor;

public sealed record TravelAdviceRequest(
    string Destination,
    int Days,
    decimal Budget,
    string TravelStyle);