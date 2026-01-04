using AppointmentGuard.API;
using AppointmentGuard.API.Mapping;              
using AppointmentGuard.API.Middlewares;
using AppointmentGuard.Data;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Data.Repositories;
using AppointmentGuard.Service.Implementations; 
using AppointmentGuard.Service.Interfaces;      
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// HttpClient servisini ekleme
builder.Services.AddHttpClient();

// Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("AppointmentGuard.Data");
    });
});

// Repository ve UnitOfWork
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPolyclinicRepository, PolyclinicRepository>();

// Service Katmanı
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPolyclinicService, PolyclinicService>();

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// Controller ve SWAGGER Ayarları
builder.Services.AddControllers();

// CORS AYARI 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()   
                   .AllowAnyMethod()   
                   .AllowAnyHeader();  
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomException();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        SeedData.Initialize(services).Wait();
    }
}
app.Run();