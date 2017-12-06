using System.Net;
using System;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("Incoming request processed to generate pairings");

    // Get request body
    var participants = await req.Content.ReadAsAsync<IEnumerable<Participant>>();
           var randomNumberGenerator = new Random();

    var gifters = participants.Select(s => s).ToList();

    foreach (var participant in participants)
    {
        var alreadyGifters = participants.Where(w => !string.IsNullOrEmpty(w.Gifter)).Select(s => s.Gifter);
        var eligibleGifters = participants.Where(w => w.Name != participant.Name && (string.IsNullOrEmpty(w.ExclusionGroupName) || w.ExclusionGroupName != participant.ExclusionGroupName) && !alreadyGifters.Contains(w.Name)).ToList();

        if (eligibleGifters.Count == 0)
        {
            log.Info($"Reached deadlock state no remaining gifters - retrying");
            break;
        }
        var randomNumber = randomNumberGenerator.Next(0, eligibleGifters.Count);
        participant.Gifter = eligibleGifters.ElementAt(randomNumber).Name;

        log.Info($"Picked {participant.Gifter} to gift for {participant.Name}");
    }

    return req.CreateResponse(HttpStatusCode.OK, participants);
}

private class Participant
{
    public string Name { get; set; }
    public string ExclusionGroupName { get; set; }
    public string Gifter { get; set; }
}
