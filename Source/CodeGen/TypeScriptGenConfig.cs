using System.Text.Json.Serialization;

namespace CodeGen;

public class TypeScriptGenConfig
{
    [JsonPropertyName("outputPath")]
    public required string OutputPath { get; init; }
    [JsonPropertyName("outputFileName")]
    public required string OutputFileName { get; init; }
    [JsonPropertyName("ignoreTypes")]
    public required List<string> IngoreTypes { get; init; } = new();
    [JsonPropertyName("namespaces")]
    public required List<NamespaceConfig> Namespaces { get; init; } = new();
}

public class NamespaceConfig
{
    [JsonPropertyName("namespace")]
    public required string Namespace { get; init; }
    [JsonPropertyName("includeDtos")]
    public required List<string> IncludeDtos { get; init; } = new();
}