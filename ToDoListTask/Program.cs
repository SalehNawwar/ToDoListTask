using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Seeders;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;
using ToDoListTask.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddControllers();

builder.Services.AddDbContext<ToDoListDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
 );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordHasher, IdentityPasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    // for swagger docs
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TodoApp API",
        Version = "v1",
        Description = "API documentation for TodoApp (Users, Auth, ToDoItems)",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Saleh Nawwar",
            Email = "nd38287@gmail.com"
        }
    });
    // Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\nExample: \"Bearer abc123\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp API v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// central exception handler and logging
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "Unhandled exception occurred");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            DuplicateEmailException or DuplicateUserNameException => StatusCodes.Status400BadRequest,
            InvalidCredentialsException => StatusCodes.Status401Unauthorized,
            UnauthorizedActionException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
    });
});

// seeder if the db is empty (the condition is inside the seeder)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToDoListDBContext>();
    DbSeeder.Seed(db);
}

app.Run();
