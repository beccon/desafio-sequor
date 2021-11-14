using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Desafio Sequor - 2021",
        Description = "Integração com o Github"
    });
});


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/user/{user}", async (string user) => {
    var httpClient = new HttpClient();

    httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add("User-Agent", "beccon");

    var response = await httpClient.GetAsync("https://api.github.com/users/" + user);
    var data = await response.Content.ReadAsStringAsync();

    return Results.Ok(data);
});

app.MapGet("v1/user/{user}/repos", async (string user) => {
    var httpClient = new HttpClient();

    httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add("User-Agent", "beccon");

    var response = await httpClient.GetAsync("https://api.github.com/users/" + user + "/repos");
    var data = await response.Content.ReadAsStringAsync();

    return Results.Ok(data);
});

app.Run();
