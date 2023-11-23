using BusinessLogicLayer.BackgroundServices;
using BusinessLogicLayer.Services;
using BusinessLogicLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataTransferObject.Contexts;
using System.Reflection;
using System.Text.Json.Serialization;
using BusinessLogicLayer.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency Injection
//// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDevicePacketRepository, DevicePacketRepository>();

// Database context
builder.Services.AddDbContext<EntityDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Auto Mapper
builder.Services.AddAutoMapper(typeof(DevicePacketProfiles));

//Services
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<DeviceService, DeviceService>();
builder.Services.AddScoped<DevicePacketService, DevicePacketService>();

//Background Services
builder.Services.AddHostedService<TcpIpBackgroundService>();

builder.Services.AddCors(builder =>
{
	builder.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
