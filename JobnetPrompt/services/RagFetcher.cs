using Newtonsoft.Json.Linq;

namespace JobnetPrompt.services;

public interface IRagFetcher
{
    Task<string> PerformRagAsync(string? query);
}

public class RagFetcher(HttpClient client) : IRagFetcher
{
    public async Task<string> PerformRagAsync(string? query)
    {
        try
        {
            var apiKey =
                "a7188fc8-13f3-460f-becd-015d78789cec<__>1OoScwETU8N2v5f42106ydVX-Pdk7IxOniqWmPj"; // Replace with your actual API key
            var baseUrl = "https://api.ydc-index.io/rag";
            var requestUri = $"{baseUrl}?query={query}?country=se";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            return json["answer"]?.ToString() ?? "No answer found";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}