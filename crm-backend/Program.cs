using crm_backend.Data;
using crm_backend.Helpers;
using crm_backend.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    // conexión a la bd y clases entidades
    services.AddDbContext<DataContext>();

    // cors
    services.AddCors();

    services.AddControllers().AddJsonOptions(x =>
    {
        // serealizar enum en response
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignorar parámetros opcionales en update
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //servicios e interfaces
    services.AddScoped<ICustomerService, CustomerService>();
}


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    // cors global
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // handle error global
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
