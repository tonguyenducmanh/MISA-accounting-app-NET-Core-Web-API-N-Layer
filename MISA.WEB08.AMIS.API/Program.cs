using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Bật CORs cho localhost
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


builder.Services.AddSwaggerGen();


// Dependencies Injection
builder.Services.AddScoped<IDepartmentBL, DepartmentBL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IPositionBL, PositionBL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IPositionDL, PositionDL>();

// Gán connection string vào trong datacontext
DataContext.MySQLConnectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Dùng CORs
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
