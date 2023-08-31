using TaskExecutor.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<NodeBusinessService>();
builder.Services.AddScoped<TaskBusinessService>();
builder.Services.AddSingleton<TaskListener>(_ => new TaskListener(builder));
var provider = builder.Services.BuildServiceProvider();
var registrar = provider.GetRequiredService<TaskListener>();

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

app.Run();