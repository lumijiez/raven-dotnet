using System.Text.Json;
using IdentityModel.Client;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(
    new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,
        ClientId = "client",
        ClientSecret = "secret"
    });

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!);

var response = await apiClient.GetAsync("https://localhost:6001/identity");

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);   
}
else
{
    Console.WriteLine(response.StatusCode);
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions
    {
        WriteIndented = true
    }));
}