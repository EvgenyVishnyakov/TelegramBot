using IRON_PROGRAMMER_BOT_ConsoleApp;
using IRON_PROGRAMMER_BOT_webhook;

var builder = WebApplication.CreateBuilder(args);

ContainerConfigurator.Configure(builder.Configuration, builder.Services);

builder.Services.AddHostedService<WebHookConfigurator>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen();
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
