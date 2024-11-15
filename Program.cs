using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configuración de la cadena de conexión
var connectionString = "Data Source=database.db";

// Endpoint para obtener todos los productos
app.MapGet("/products", async () =>
{
    using var connection = new SQLiteConnection(connectionString);
    await connection.OpenAsync();

    var command = new SQLiteCommand("SELECT Id, Name, Price FROM Products", connection);
    var reader = await command.ExecuteReaderAsync();

    var products = new List<dynamic>();
    while (await reader.ReadAsync())
    {
        products.Add(new
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Price = reader.GetDouble(2)
        });
    }

    return Results.Ok(products);
});

app.Run();
