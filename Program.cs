using CarSaloonMinimalAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/car", async (DataContext dataContext) => await dataContext.Cars.ToListAsync());


app.MapGet("/car/{id}", async (DataContext dataContext, int id) =>
    await dataContext.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound("Car not found!"));


app.MapPost("/car", async (DataContext dataContext, Car car) =>
{
    dataContext.Cars.Add(car);
    await dataContext.SaveChangesAsync();
    return Results.Ok(await dataContext.Cars.ToListAsync());
});
    

app.MapPut("/car/{id}", async (DataContext dataContext,int id, Car c) =>
{
    var car = await dataContext.Cars.FindAsync(id);
    if (car == null)
    {
        return Results.NotFound("Car not found");
    }

    car.Id = c.Id;
    car.Name = c.Name;
    car.Brand= c.Brand;
    car.DefaultColor = c.DefaultColor;
    car.Price = c.Price;

    await dataContext.SaveChangesAsync();

    return Results.Ok(await dataContext.Cars.ToListAsync());

});

app.MapDelete("/car", async (DataContext dataContext, int id) =>
{
    var car = await dataContext.Cars.FindAsync(id);
    if (car == null)
    {
        return Results.NotFound("Car not found");
    }

    dataContext.Cars.Remove(car);

    await dataContext.SaveChangesAsync();

    return Results.Ok(await dataContext.Cars.ToListAsync());
});


app.Run();

public class Car
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Name { get; set; }
    public string? DefaultColor { get; set; }
    public int Price { get; set; }

}