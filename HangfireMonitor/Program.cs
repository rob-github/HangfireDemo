using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer(config =>
{
    config.ServerName = Environment.MachineName;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

BackgroundJob.Schedule(() => Console.WriteLine("Delayed 'Hello World!', from Hangfire at {0}.", DateTime.Now.ToString("F")), TimeSpan.FromMinutes(2));
BackgroundJob.Enqueue(() => Console.WriteLine("Hello World, from Hangfire at {0}.", DateTime.Now.ToString("F")));

app.Run();
