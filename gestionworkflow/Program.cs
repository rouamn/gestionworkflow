using gestionworkflow;
using gestionworkflow.Commands;
using gestionworkflow.Context;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextName>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextName")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(LibraryEntrypoint).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowReactClient");
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.UseHttpsRedirection();

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    var users = await userRepository.GetAllUsersAsync();
    return users;
});
app.MapGet("/users/{id}", async ([FromServices] IUserRepository userRepository, int id) =>
{
    var user = await userRepository.GetAvanceQuery(id);
    if (user != null)
    {
        return Results.Ok(user);
    }
    else
    {
        return Results.NotFound();
    }
});


app.MapPost("/users", async (context) =>
{
    var userRepository = context.RequestServices.GetRequiredService<IUserRepository>();
    var user = await context.Request.ReadFromJsonAsync<User>();
    await userRepository.CreateUserAsync(user);
    context.Response.StatusCode = 201;
    context.Response.Headers.Location = $"/users/{user.Id}";
    return;
});
app.MapPut("/create", async (IUserRepository userRepository, User user) =>
{
    await userRepository.CreateUserAsync(user);
    return Results.Created($"/users/{user.Id}", user);
});
app.MapPut("/Addconge", async (IUserRepository userRepository, DemandeConge demandeConge) =>
{
    await userRepository.AddDemandeCongeAsync(demandeConge);
    return Results.Created($"/demandeConge/{demandeConge.Id}", demandeConge);
});
app.MapPut("/Addavance", async (IUserRepository userRepository, DemandeAvance demandeAvance) =>
{
    await userRepository.AddDemandeAvanceAsync(demandeAvance);
    return Results.Created($"/demandeAvance/{demandeAvance.UtilisateurId}", demandeAvance);
});


app.MapPost("/update/{id}", async (IUserRepository userRepository, int id, User user) =>
{
    var existingUser = await userRepository.GetAvanceQuery(id);
    if (existingUser == null)
    {
        return Results.NotFound();
    }
    user.Id = id;
    await userRepository.UpdateUserAsync(user);
    return Results.NoContent();
});
app.MapPost("/login", async (IMediator mediator, LoginUserCommand command) =>
{
    var user = await mediator.Send(command);
    if (user == null)
    {
        return Results.BadRequest("Invalid email or password");
    }
    return Results.Ok(user);
});


app.MapDelete("/delete/{id}", async (IUserRepository userRepository, int id) =>
{
    var existingUser = await userRepository.GetAvanceQuery(id);
    if (existingUser == null)
    {
        return Results.NotFound();
    }
    await userRepository.DeleteUserAsync(id);
    return Results.NoContent();
});
app.MapGet("/pending", async (IUserRepository userRepository) =>
{
    var users = await userRepository.GetPendingUsersAsync();
    return Results.Ok(users);
});

app.MapPut("/approv{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateUserStatusCommand { Id = userId, Status = "Approved" });

    // Send notification to user about the approval of their registration
    // ...

    return Results.Ok();
});
app.MapGet("/user/{UserId}/demandeconge", async (IUserRepository userRepository, IMediator _mediator, int UserId) =>
{
    var user = await userRepository.GetAvanceQuery(UserId);

    if (user == null)
    {
        return Results.NotFound();
    }

    var query = new GetDemandeCongeQuery { UserId = UserId };
    var leaveRequests = await _mediator.Send(query);

    return Results.Ok(leaveRequests);
});

app.MapPut("/reject/{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateUserStatusCommand { Id = userId, Status = "Rejected" });

    // Send notification to user about the rejection of their registration
    // ...

    return Results.Ok();
});
//demande aprove  pending reject 
app.MapGet("/pendingDemande", async (IUserRepository userRepository) =>
{
    var demandes = await userRepository.GetPendingDemandesAsync();
    return Results.Ok(demandes);
});

app.MapPut("/approveDemande/{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateDemandeStatusCommand { Id = userId, Statut = "Approved" });

    // Send notification to user about the approval of their registration
    // ...

    return Results.Ok();
});
app.MapPut("/rejectDemande/{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateDemandeStatusCommand { Id = userId, Statut = "Rejected" });

    // Send notification to user about the rejection of their registration
    // ...

    return Results.Ok();
});

//avance aprove  pending reject 
app.MapGet("/pendingAvance", async (IUserRepository userRepository) =>
{
    var demandes = await userRepository.GetPendingAvancesAsync();
    return Results.Ok(demandes);
});

app.MapPut("/approveAvance/{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateAvanceStatusCommand { Id = userId, Statut = "Approved" });

    // Send notification to user about the approval of their registration
    // ...

    return Results.Ok();
});
app.MapPut("/rejectAvance/{userId}", async (IMediator mediator, int userId) =>
{
    await mediator.Send(new UpdateAvanceStatusCommand { Id = userId, Statut = "Rejected" });

    // Send notification to user about the rejection of their registration
    // ...

    return Results.Ok();
});





app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}