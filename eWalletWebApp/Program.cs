using eWalletWebApp.EfCore;
using eWalletWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.---

// Datasource is EF Core
builder.Services.AddDbContext<EWalletContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("EWalletConnection")));

//Enable CORS
builder.Services.AddCors(c => {
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddControllers();

//Add logging service
builder.Services.AddHostedService<AccountLoggingService>();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs\\AccountLog-.txt",rollingInterval:RollingInterval.Day).CreateLogger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.---

//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//JSON Serializer - Not working, will comment out for now
// builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
//         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//     .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
//         = new DefaultContractResolver());

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();