using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.BusinessLogic.Mappers;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.BusinessLogic.Services.RunCreationStrategy;
using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddScoped<IRepository<Run>, BaseRepository<Run>>();
builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();
builder.Services.AddScoped<IRepository<Model>, BaseRepository<Model>>();

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

builder.Services.AddTransient<IRunCreationStrategy, OpenAICreationStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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