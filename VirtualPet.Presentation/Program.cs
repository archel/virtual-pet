using VirtualPet.Application.Commands;
using VirtualPet.Application.Queries;
using VirtualPet.Domain.Pet;
using VirtualPet.Infrastructure.Pet;
using VirtualPet.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails(o => o.CustomizeProblemDetails = c =>
        c.ProblemDetails.Instance = c.HttpContext.Request.Path);
builder.Services.AddTransient<ICommandHandler<CreatePetCommand>, CreatePetCommandHandler>();
builder.Services.AddTransient<IQueryHandler<GetPetQuery, PetDto>, GetPetQueryHandler>();
builder.Services.AddSingleton<IPetRepository, InMemoryPetRepository>();
builder.Services.AddTransient<IGuidGenerator, GuidGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapVirtualPetEndpoints();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }