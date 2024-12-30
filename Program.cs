using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using TimeMangementSystemAPI.Middlewares;
using TimeMangementSystemAPI.Repository;
using TimeMangementSystemAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<Clientservice>();
builder.Services.AddScoped<projectInterface, ProjectService>();
builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<IUserDetails,UserRepository>();   
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<Aeth>();

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
 ValidateIssuer = true,
 ValidateAudience = true,
 ValidateLifetime = true,
 ValidateIssuerSigningKey = true,
 ValidIssuer = builder.Configuration["Jwt:Issuer"],
 ValidAudience = builder.Configuration["Jwt:Audience"],
 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
};
});
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseMiddleware<JWTMiddleware>(

        builder.Configuration["Jwt:Key"],

        builder.Configuration["Jwt:Issuer"],

        builder.Configuration["Jwt:Audience"]

    );


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
