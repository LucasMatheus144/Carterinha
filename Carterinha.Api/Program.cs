using System.Configuration;
using NHibernate.Cfg;
using Carterinha.Aplication.Services;
using Carterinha.Aplication.Repository;
using Carterinha.Aplication.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CarterinhaService>();
builder.Services.AddScoped<ValidatorService>();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton(c =>
{
    var config = new NHibernate.Cfg.Configuration().Configure();
    config.DataBaseIntegration(
        x => x.ConnectionString = connectionString
    );
    return config.BuildSessionFactory();
});
builder.Services.AddTransient(typeof(IRepository<>), typeof(RepositoryImplementation<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
