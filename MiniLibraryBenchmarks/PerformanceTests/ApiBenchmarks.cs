using System.Net.Http.Json;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MiniLibraryBenchmar.PerformanceTests;

[MemoryDiagnoser]
public class ApiBenchmarks
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [GlobalSetup]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [Benchmark]
    public async Task GetCategories()
    {
        await _client.GetFromJsonAsync<object>("/api/Category");
    }
}