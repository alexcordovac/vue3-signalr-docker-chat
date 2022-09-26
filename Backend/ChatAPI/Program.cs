using ChatAPI.Hubs;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var AllowSignalR = "_signalr";

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          //policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                          policy
                          .WithOrigins("http://localhost:5173", "https://localhost:5173", "http://127.0.0.1:5173", "https://127.0.0.1:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();


app.MapControllers();

//app.MapHub<ChatHub>("/ChatHub");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/ChatHub");
});


app.Run();
