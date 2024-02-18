using ConfigsLoaders;
using ConfigsLoaders.Interfaces;
using ConfigurationService.Database.Entities;
using ConfigurationService.Database.Extensions;
using ConfigurationService.Entities.Logic;
using ConfigurationService.Entities.Repositories.Interfaces;
using IdentityServer4.AccessTokenValidation;
using LoggerLib.Loggers;
using MessagesLib.Entities;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    var converter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(converter);
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


IConfigsLoader loader = new EnvironmentLoader();
LoggerLib.Interfaces.ILogger logger = new ConsoleLogger();
#if DEBUG
loader = new ImMemoryLoader();
#endif

var rabbit = loader.GetRabbitConfiguration();
var postgresql = loader.GetDbConfiguration();

#if DEBUG
postgresql.DataBase = "configs";
#endif



builder.Services.AddScoped<IProgramsRepository, ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Repository>();
builder.Services.AddScoped<IProvidersRepository, ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Repository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<LoggerLib.Interfaces.ILogger>(logger);
builder.Services.AddMigratorWithProvider(new DataBaseConfig()
{
    Host = postgresql.Host,
    Password = postgresql.Password,
    Username = postgresql.Username,
    Port = postgresql.Port,
    DataBase = postgresql.DataBase
},Assembly.GetExecutingAssembly());


builder.Services.AddSingleton<Senders>(new Senders(new RabbitConfiguration()
{
    Host = rabbit.Host,
    Password = rabbit.Password,
    Port = rabbit.Port,
    UserName = rabbit.UserName
},logger));

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(options =>
   {
       options.RequireHttpsMetadata = false;
       options.Authority = Environment.GetEnvironmentVariable("identety_url");
   });



builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Migrate();
app.Run();
