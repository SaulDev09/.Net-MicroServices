using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SC.MessageBus;
using SC.Services.AuthAPI.Data;
using SC.Services.AuthAPI.Models;
using SC.Services.AuthAPI.Service;
using SC.Services.AuthAPI.Service.IService;

var builder = WebApplication.CreateBuilder(args);

#region [Setting up EF - INIT]
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

#region [Identity]
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion
// Add services to the container.

#region [MessageBus]
builder.Services.AddScoped<IMessageBus, MessageBus>();
#endregion

builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // https://localhost:7002/swagger/index.html
    if (!app.Environment.IsDevelopment())
    // https://localhost:7002/index.html
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthAPI");
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}