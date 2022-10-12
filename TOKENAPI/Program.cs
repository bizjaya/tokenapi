using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer.Crypto;
using System.Configuration;
using System.IO.Compression;
using System.Net;
using System.Text.Json;
using TOKENAPI;
using TOKENAPI.BG;
using TOKENAPI.EF;
using TOKENAPI.Mapper;
using TOKENAPI.Models;
using TOKENAPI.Service;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

var builder = WebApplication.CreateBuilder(args);

    string dbconn = builder.Configuration.GetConnectionString("devcon");
    builder.Services.AddDbContextFactory<DbCtx>(builder => builder
    .UseMySql(dbconn, ServerVersion.AutoDetect(dbconn))
    );

    builder.Services.AddDbContext<DbCtx>();

builder.Services.AddCors();

  //  builder.Services.AddCors(options =>
  //  {
        //options.AddPolicy("CorsPolicy",
        //    builder =>
        //    builder
        //    .WithOrigins("http://localhost:3000", "https://localhost:44357") //.AllowAnyOrigin()
        //    .AllowAnyMethod()
        //    .WithExposedHeaders("content-disposition")
        //    .AllowAnyHeader()
        //    .AllowCredentials()
        //    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
   // });


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHostedService<QueueSvc>();
builder.Services.AddSingleton<IBGTaskQueue, BgTaskQueue>();




builder.Services.AddSingleton(typeof(DbCon));
builder.Services.AddSingleton(builder.Configuration.GetSection("Stgs").Get<Stgs>());
builder.Services.AddSingleton<ICont,Cont>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAcctService , AcctService>();

builder.Services.AddHangFire(dbconn);
builder.Services.AddMappers();
builder.Services.AddCompression();


//var mappingConfig = new MapperConfiguration(mc => {
//    mc.AddProfile(new EvtProfile());
//});

//builder.Services.AddSingleton(mappingConfig.CreateMapper());


//builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();


var recurringJobManager = app.Services.GetService<IRecurringJobManager>();

recurringJobManager.AddOrUpdate<IAcctService>("Run Every3M", job => job.Every3M(), "*/3 * * * *", TimeZoneInfo.Local);
recurringJobManager.AddOrUpdate<IAcctService>("Run Every5M", job => job.Every1H(), "*/5 * * * *", TimeZoneInfo.Local);
recurringJobManager.AddOrUpdate<IAcctService>("Run Every10M", job => job.Every10M(), "*/10 * * * *", TimeZoneInfo.Local);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseHttpsRedirection();

// app.UseCors("CorsPolicy");

app.UseCors(builder => builder
   .AllowAnyHeader()
   .AllowAnyMethod()
   .SetIsOriginAllowed((host) => true)
   .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
