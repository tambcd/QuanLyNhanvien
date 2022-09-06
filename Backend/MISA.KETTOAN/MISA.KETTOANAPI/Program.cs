using MISA.BLogicLayer.Interface;
using MISA.BLogicLayer.Services;
using MISA.Common.Entities;
using MISA.DAL.Interface;
using MISA.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// DL interface
/// </summary>
builder.Services.AddScoped<IRepository<object>, BaseRepository<object>>();
builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
builder.Services.AddScoped<IPositionsRepository,PositionRepository>();
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

/// <summary>
/// BLL Interface
/// </summary>
builder.Services.AddScoped<IBaseBLL<object>, BaseBLL<object>>();
builder.Services.AddScoped<IDepartmentBLL, DepartmentBLL>();
builder.Services.AddScoped<IPositionsBLL, PositionsBLL>();
builder.Services.AddScoped<IEmployeeBLL, EmployeeBLL>();




/*builder.Services.Add.AddScoped<>
*/
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();
app.Run();

//



