using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Referentiel.Application;
using Referentiel.Infrastructure;
using Referentiel.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin() // Autoriser n'importe quelle origine
            .AllowAnyMethod() // Autoriser n'importe quelle méthode (GET, POST, etc.)
            .AllowAnyHeader()); // Autoriser n'importe quel en-tête
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddApplicationInjection();
builder.Services.AddInfrastructureInjection();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.MetadataAddress = builder.Configuration["ClientCredentials:MetadataAddress"];
    options.SaveToken = true;
    options.Audience = builder.Configuration["ClientCredentials:ClientId"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Ajouter la configuration CORS ici
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
