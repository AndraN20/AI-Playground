using AiPlayground.BusinessLogic.AIProcessing.Factories;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.BusinessLogic.Mappers;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AIPlaygroundContext");

builder.Services.AddDbContext<AiPlaygroundContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IRepository<Scope>, ScopeRepository>();
builder.Services.AddScoped<IRepository<Prompt>, BaseRepository<Prompt>>();
builder.Services.AddScoped<IPromptRepository, PromptRepository>();
builder.Services.AddScoped<IRepository<Run>, BaseRepository<Run>>();
builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();
builder.Services.AddScoped<IRepository<Model>, BaseRepository<Model>>();
builder.Services.AddScoped<IRunRepository, RunRepository>();


builder.Services.AddScoped<IScopeService, ScopeService>();
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IRunService, RunService>();


builder.Services.AddScoped<IModelMapper, ModelMapper>();
builder.Services.AddScoped<IScopeMapper, ScopeMapper>();
builder.Services.AddScoped<IPromptMapper, PromptMapper>();
builder.Services.AddScoped<IPlatformMapper, PlatformMapper>();
builder.Services.AddScoped<IRunMapper, RunMapper>();

builder.Services.AddSingleton<RatingService>();

builder.Services.AddScoped<IAIProcessorFactory, AIProcessorFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();