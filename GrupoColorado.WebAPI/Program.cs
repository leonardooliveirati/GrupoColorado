using GrupoColorado.Domain.Entities;
using GrupoColorado.Application.Interfaces;
using GrupoColorado.Application.Services;
using GrupoColorado.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:5002")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Serviços MVC e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de contexto e dependências
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DbGrupoColorado"));

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Seed manual de TiposTelefone
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.TiposTelefone.Any())
    {
        context.TiposTelefone.AddRange(
            new TipoTelefone { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Residencial", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" },
            new TipoTelefone { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Comercial", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" },
            new TipoTelefone { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "WhatsApp", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" }
        );
        context.SaveChanges();
    }
}

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();