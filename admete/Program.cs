using admete.Controllers;
using admete.MockedDatabase;
using admete.Repos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IDatabaseService, DatabaseService>();
builder.Services.AddTransient<ILeekieService, LeekieService>();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
