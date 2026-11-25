using DSW_3P_PR03.Services;
using DSW_3P_PR03.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Agregar controladores
builder.Services.AddControllers();

// Agregar dependencias
builder.Services.AddScoped<IPersonajeRepository,PersonajeJsonRepository>();
builder.Services.AddScoped<IPersonajeService,PersonajeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "CharacterHub API v1"
        );
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
