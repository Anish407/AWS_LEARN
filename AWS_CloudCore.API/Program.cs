using Amazon.Runtime;
using AWS_CloudCore.Core.Models.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



string secretsManagerEnvironmentKey = $"{builder.Environment.EnvironmentName}_{builder.Environment.ApplicationName}_";
builder.Configuration.AddSecretsManager(region: Amazon.RegionEndpoint.EUNorth1, configurator: config =>
{
    // only load secrets for a particular environment
    // secret name format: {env}_ProjectName_Key 
    //eg:  Production_AWS_CloudCore.API_SNSConfig__Endpoint
    config.SecretFilter = entry => entry.Name.StartsWith(secretsManagerEnvironmentKey);
    config.KeyGenerator = (entry, name) => name
                                                     .Replace(secretsManagerEnvironmentKey, string.Empty) // remove the prefix from the secrets
                                                     .Replace("__", ":"); // map to IConfigurationProvider format

    config.PollingInterval = TimeSpan.FromSeconds(60);
});
builder.Configuration.AddSecretsManager();


builder.Services.Configure<SNSConfig>(builder.Configuration.GetSection("SNSConfig"));





var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
