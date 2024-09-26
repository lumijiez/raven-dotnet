using System.Text.Json;

namespace Raven.BFF.Application.Models;

public class Result
{
    public bool? Error  { get; set; }
    public bool? Success { get; set; }
    public string? Message { get; set; }
    public JsonElement? Data { get; set; }
}