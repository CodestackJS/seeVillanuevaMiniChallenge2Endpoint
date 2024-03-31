var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Mini Challenge 2 Endpoint
// Endpoint that accepts 2 numbers:
app.MapGet("/accepts2Numbers",(int num1, int num2) => {
    return  num1 + num2 + " is the sum of " + num1 + " and " + num2 +".";
});

// Endpoint that accepts 2 inputs:
app.MapGet("/accepts2Inputs",(string name, int time, string amOrpm) => {
    return name + " woke up at " + time + amOrpm +".";
});

//Endpoint that accepts 2 showing >, < or =
app.MapGet("/greaterLesserThanEquals", (int num1, int num2) => {
    string result;
    if(num1 > num2)
    {
        result = num1 + " is greater than " + num2 + " so " + num2 + " is less than " + num1 + ".";
    }
    else if (num1 < num2){
        result = num1 + " is less than " + num2 + " so " + num2 + " is greater than " + num1 + ".";
    }
    else
    {
        result = num1 + " is equal to " + num2 + " so " + "both numbers are equal.";
    }
    return result;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
