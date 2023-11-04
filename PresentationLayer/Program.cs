using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataTransferObject.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency Injection
// Repository
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDeviceRepository, DeviceRepository>();
builder.Services.AddTransient<IDevicePacketRepository, DevicePacketRepository>();

// Database context
builder.Services.AddDbContext<EntityDbContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<DeviceService, DeviceService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
